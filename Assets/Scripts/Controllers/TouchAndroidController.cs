using UnityEngine;

namespace Controllers
{
    public class TouchAndroidController :BaseController
    {
        private DataPoint[] _current;
        public override void Update()
        {
            _current = new DataPoint[Input.touchCount];
            if (Input.touchCount > 0)
            {
                _isActive = true;
                for (int i = 0; i < _current.Length; i++)
                {
                    Touch currenTouch = Input.touches[i];
                    Vector2 pointPostion = PointToWorldPoint(currenTouch.position);
                    _current[i] = new DataPoint(pointPostion, true);
                }
            }
            else
            {
                _isActive = false;
            }
        }

        public override DataPoint[] GetInputs()
        {
            return _current;
        }
    }
}