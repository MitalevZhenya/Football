using UnityEngine;

namespace Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        public abstract Vector2 Axis { get; }
        public abstract bool IsThrowBall();

        protected static Vector2 MobileAxis() =>
            new Vector2(FixedTouchField.Instance.TouchDist.x, FixedTouchField.Instance.TouchDist.y);
        protected static bool MobilePressed() => FixedTouchField.Instance.IsPressed;
    }
}