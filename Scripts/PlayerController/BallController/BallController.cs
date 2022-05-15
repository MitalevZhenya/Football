using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private Rigidbody ballRB;
    [SerializeField] private DrawerTrojectory drawerCurve;
    [SerializeField] private float ballSpeed;
    [SerializeField] private PathType pathMode;
    private Vector3[] currPositions;
    public void StartMoveBall() => InvokeMoveBall(drawerCurve.Positions);
    private void InvokeMoveBall(Vector3[] _positions)
    {
        ball.position = _positions[0];
        ballRB.isKinematic = true;
        MoveBall(_positions);
    }
    private void MoveBall(Vector3[] _positions)
    {
        print($"start path");
        ball.DOPath(_positions, ballSpeed, pathMode);
        currPositions = _positions;
    }
    public void StopMoveBall()
    {
        print($"stop move");
        ball.DOPause();
    }
    public void ThrowBall()
    {
        print($"throw ball");
        StopMoveBall();
        ballRB.isKinematic = false;
        ball.LookAt(currPositions[currPositions.Length - 1]);
        ballRB.AddForce(-ball.forward, ForceMode.Impulse);
    }
    public void SetBall(Transform _ball, Rigidbody _ballRB)
    {
        ball = _ball;
        ballRB = _ballRB;
    }
}