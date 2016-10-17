using UnityEngine;

namespace Controllers
{
    public class TouchAndroidController :BaseController
    {
        private InputData[] _currentInputs;
        public override void Update()
        {
            _currentInputs = new InputData[Input.touchCount];
            if (Input.touchCount > 0)
            {
                _isActive = true;
                for (int i = 0; i < _currentInputs.Length; i++)
                {
                    Touch currenTouch = Input.touches[i];
                    Vector2 pointPostion = PointToWorldPoint(currenTouch.position);
                    _currentInputs[i] = new InputData(pointPostion, true);
                }
            }
            else
            {
                _isActive = false;
            }
        }

        public override InputData[] GetInputs()
        {
            return _currentInputs;
        }
    }
}