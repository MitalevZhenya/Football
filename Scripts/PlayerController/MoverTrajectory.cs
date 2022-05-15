using System.Collections;
using Infrastructure;
using UnityEngine;
using Services.Input;

public class MoverTrajectory : MonoBehaviour
{
    [SerializeField] private Transform centerPoint, endPoint;
    [SerializeField] private float speedCenter, speedEnd;
    [Header("Values X Pos")]
    [SerializeField] private float minPosXCenter;
    [SerializeField] private float maxPosXCenter;
    [SerializeField] private float minPosXEnd, maxPosXEnd;
    [Header("Values Y Pos")]
    [SerializeField] private float minPosYCenter;
    [SerializeField] private float maxPosYCenter;
    [SerializeField] private float minPosYEnd, maxPosYEnd;

    public void MovePoints(float _horizontal, float _vertical)
    {
        centerPoint.Translate(-_horizontal * Time.deltaTime * speedCenter, _vertical * Time.deltaTime * speedCenter, 0);
        endPoint.Translate(-_horizontal * Time.deltaTime * speedEnd, _vertical * Time.deltaTime * speedEnd, 0);

        ClampedValues();
    }
    private void ClampedValues()
    {
        Vector3 _posCenterPoint = centerPoint.localPosition;
        _posCenterPoint.x = Mathf.Clamp(centerPoint.localPosition.x, minPosXCenter, maxPosXCenter);
        _posCenterPoint.y = Mathf.Clamp(centerPoint.localPosition.y, minPosYCenter, maxPosYCenter);
        centerPoint.localPosition = _posCenterPoint;

        Vector3 _posEndPoint = endPoint.localPosition;
        _posEndPoint.x = Mathf.Clamp(endPoint.localPosition.x, minPosXEnd, maxPosXEnd);
        _posEndPoint.y = Mathf.Clamp(endPoint.localPosition.y, minPosYEnd, maxPosYEnd);
        endPoint.localPosition = _posEndPoint;
    }
    public void SetPoints(Transform _centerPoint, Transform _endPoint)
    {
        centerPoint = _centerPoint;
        endPoint = _endPoint;
    }
}