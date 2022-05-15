using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerTrojectory : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int numPoints;

    [SerializeField] private Transform startPoint, centerPoint, endPoint;
    private Vector3[] positions;
    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[numPoints];

        lineRenderer.positionCount = numPoints;
    }

    // Update is called once per frame
    void Update()
    {
        DrawQuadraticCurve();
    }
    private void DrawQuadraticCurve()
    {
        if (startPoint == null || centerPoint == null || endPoint == null) return;

        for (int i = 1; i <= numPoints; i++)
        {
            float _t = i / (float)numPoints;
            positions[i - 1] = CalculateQuadraticBezierPoint(_t, startPoint.position, centerPoint.position, endPoint.position);
        }
        lineRenderer.SetPositions(positions);
    }
    private Vector3 CalculateQuadraticBezierPoint(float _t, Vector3 _p0, Vector3 _p1, Vector3 _p2)
    {
        float _u = 1 - _t;
        float _tt = _t * _t;
        float _uu = _u * _u;
        Vector3 _p = _uu * _p0;
        _p += 2 * _u * _t * _p1;
        _p += _tt * _p2;
        return _p;
    }
    public Vector3[] Positions => positions;
    public void SetPoints(Transform _startPoint, Transform _centerPoint, Transform _endPoint)
    {
        startPoint = _startPoint;
        centerPoint = _centerPoint;
        endPoint = _endPoint;
        if (lineRenderer == null) return;
        for(int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, Vector3.zero);
        }
    }
    public void SetDrawState(bool _state) => lineRenderer.gameObject.SetActive(_state);
}