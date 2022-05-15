using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandlerCollision : MonoBehaviour
{
    [SerializeField] private RegisterCollision registerCollision;
    private Dictionary<Type, InspectCollisionDelegate> collisionHandlers;

    private void Awake()
    {
        RegisterHndlers();
    }
    private void RegisterHndlers()
    {
        collisionHandlers = new Dictionary<Type, InspectCollisionDelegate>()
        {
            [typeof(GatesObstacles)] = BallInGate,
            [typeof(DestroyedObstacle)] = BallDestroyObstacle,
            [typeof(FailedLevelObstacle)] = FailedLevel
        };
    }
    private void InspectionCollision(BaseObstacles _obstacles)
    {
        Type _obstacleType = _obstacles.GetType();
        if (collisionHandlers.ContainsKey(_obstacleType))
        {
            collisionHandlers[_obstacleType].Invoke(_obstacles);
            print($"{collisionHandlers[_obstacleType].Method.Name}");
            _obstacles.PlayParticle();
        }
    }
    public void SetRegisterCollision(RegisterCollision _register)
    {
        if(registerCollision != null)
            registerCollision.OnCollisionEvent -= InspectionCollision;
        _register.OnCollisionEvent += InspectionCollision;
        registerCollision = _register;
    }
    #region BallCollisionMethods
    private void BallInGate(BaseObstacles _obstacle) => BallInGateEvent?.Invoke();
    private void BallDestroyObstacle(BaseObstacles _obstacle)
    {
        DestroyedObstacle _destroyedObstacle = (DestroyedObstacle)_obstacle;
        _destroyedObstacle.DestroyedVersion.SetActive(true);
        _destroyedObstacle.gameObject.SetActive(false);
    }
    private void FailedLevel(BaseObstacles _obstacle)
    {
        FailLevelEvent?.Invoke();
        print($"fail");
    }
    #endregion
    public event Action BallInGateEvent;
    public event Action FailLevelEvent;
    private delegate void InspectCollisionDelegate(BaseObstacles _obstacle);
}