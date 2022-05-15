using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    #region PublicValues
    [field:SerializeField] public Transform MoveTarget { get; private set; }
    [field:SerializeField] public RegisterCollision RegisterCollision { get; private set; }
    [field:SerializeField] public Rigidbody BallRB { get; private set; }
    [field:SerializeField] public Transform BallTransform { get; private set; }
    [field:SerializeField] public Transform StartPoint { get; private set; }
    [field:SerializeField] public Transform CenterPoint { get; private set; }
    [field:SerializeField] public Transform EndPoint { get; private set; }
    [field:SerializeField] public ReaderAnimationEvents ReaderEvents { get; private set; }
    [field:SerializeField] public ModelAnimator ModelAnimator { get; private set; }
    #endregion
    [SerializeField] private DestroyedObstacle[] destroyedObstacles;
    private Vector3 centerPointPos, endPointPos;
    private void Awake()
    {
        centerPointPos = CenterPoint.position;
        endPointPos = EndPoint.position;
    }
    public void RegenerateLocation()
    {
        BallRB.isKinematic = true;
        CenterPoint.position = centerPointPos;
        EndPoint.position = endPointPos;
        BallTransform.position = StartPoint.position;
        ModelAnimator.SetDefaultPosModel();
        for (int i = 0; i < destroyedObstacles.Length; i++)
        {
            destroyedObstacles[i].gameObject.SetActive(true);
            destroyedObstacles[i].InstantiateDestroyVersion();
        }
    }
}