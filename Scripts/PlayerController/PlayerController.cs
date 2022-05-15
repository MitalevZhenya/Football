using Services.Input;
using UnityEngine;
using System.Collections;
using Infrastructure;

public class PlayerController : MonoBehaviour
{
    [Header("Switch State")]
    [SerializeField] private LocationHolder locationHolder;
    [SerializeField] private Fader fader;
    [Header("Switch Scene")]
    [SerializeField] private Fader loadSceneFader;
    [Header("Mechanics")]
    [SerializeField] private DrawerTrojectory drawerTrojectory;
    [SerializeField] private MoverTrajectory moverTrajectory;
    [SerializeField] private BallController ballController;
    [Header("Ball Events")]
    [SerializeField] private HandlerCollision handlerCollision;
    [Header("Model Animation")]
    [SerializeField] private ModelAnimator modelAnimator;
    [SerializeField] private ReaderAnimationEvents readerEvents;

    private IInputService inputService;
    private bool pressed, notMoved;
    private Location currLocation, targetLocation;
    private int indexLocation;
    private void Awake()
    {
        inputService = Game.InputService;
        handlerCollision.BallInGateEvent += CheckProgress;
        handlerCollision.FailLevelEvent += FailedLevel;
    }
    private void Start()
    {
        RegisterLocation(locationHolder[0], false);
    }

    private void Update()
    {
        MoveCurve();
        ThrowBall();
    }
    private void CheckProgress()
    {
        indexLocation++;
        ballController.ThrowBall();
        if (indexLocation > locationHolder.LocationAmount)
        {
            CompleteLevel();
        }
        else
        {
            RegisterLocation(locationHolder[indexLocation], true);
        }
    }
    private void CompleteLevel()
    {
        print("complete level");
        handlerCollision.FailLevelEvent -= FailedLevel;
        loadSceneFader.StartFade(SceneController.Instance.LoadNextScene);
    }
    private void MoveCurve()
    {
        float _horizontal = inputService.Axis.x;
        float _vertical = inputService.Axis.y;

        if (!notMoved) return;
        moverTrajectory.MovePoints(_horizontal, _vertical);
    }
    private void ThrowBall()
    {
        if (!notMoved) return;

        if (inputService.IsThrowBall()) pressed = true;

        if (!inputService.IsThrowBall() && pressed)
        {
            modelAnimator.SetThrowBallAnimation();
            drawerTrojectory.SetDrawState(false);
            pressed = false;
        }
    }
    private void StartThrowBall()
    {
        print($"throw ball");
        ballController.StartMoveBall();
    }
    private void RegisterLocation(Location _location, bool _loadFade)
    {
        if(readerEvents != null)
            readerEvents.ThrowBallEvent -= StartThrowBall;
        if (_location == null) return;
        
        notMoved = false;
        readerEvents = _location.ReaderEvents;
        modelAnimator = _location.ModelAnimator;
        readerEvents.ThrowBallEvent += StartThrowBall;
        ballController.StopMoveBall();
        handlerCollision.SetRegisterCollision(_location.RegisterCollision);
        ballController.SetBall(_location.BallTransform, _location.BallRB);
        moverTrajectory.SetPoints(_location.CenterPoint, _location.EndPoint);
        drawerTrojectory.SetPoints(_location.StartPoint, _location.CenterPoint, _location.EndPoint);

        targetLocation = _location;

        if (_loadFade) fader.StartFade(SetNextState);
        else SetNextState();
    }
    private void SetNextState()
    {
        if (currLocation != null)
            locationHolder.DisactivateLocation(currLocation);
        currLocation = targetLocation;
        locationHolder.ActivateLocation(currLocation);
        locationHolder.RegenerateLocation(currLocation);
        drawerTrojectory.SetDrawState(true);
        notMoved = true;
    }
    private void FailedLevel()
    {
        print($"load 0 level");
        ballController.ThrowBall();
        indexLocation = 0;
        RegisterLocation(locationHolder[indexLocation], true);
    }
}