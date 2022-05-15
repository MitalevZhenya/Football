using UnityEngine;

namespace Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 _axis = MobileAxis();
                if (_axis == Vector2.zero)
                {
                    _axis = UnityAxis();
                }
                return _axis;
            }
        }
        public override bool IsThrowBall()
        {
            bool _pressed = MobilePressed();
            if (!_pressed)
            {
                _pressed = UnityPressed();
            }
            return _pressed;
        }

        private static Vector2 UnityAxis() =>
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
        private static bool UnityPressed() => UnityEngine.Input.GetKeyDown(KeyCode.Space);
    }
}