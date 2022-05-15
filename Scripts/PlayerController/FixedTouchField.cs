using UnityEngine.EventSystems;
using UnityEngine;

public class FixedTouchField : MonoBehaviourSingleton<FixedTouchField>, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 TouchDist { get; private set; }
    private Vector2 pointerOld;
    private int pointerId;
    public bool IsPressed { get; private set; }

    void Update()
    {
        if (IsPressed)
        {
            if (pointerId >= 0 && pointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[pointerId].position - pointerOld;
                pointerOld = Input.touches[pointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - pointerOld;
                pointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDist = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        pointerId = eventData.pointerId;
        pointerOld = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
    }
}