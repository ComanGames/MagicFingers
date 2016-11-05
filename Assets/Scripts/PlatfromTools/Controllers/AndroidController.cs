using UnityEngine;

namespace PlatfromTools.Controllers
{
    public class AndroidController :AbstractController
    {
        private DataPoint[] _current;
        public override void Update()
        {
            _current = new DataPoint[Input.touchCount];
            if (Input.touchCount > 0)
            {
                IsActivated = true;
                for (int i = 0; i < _current.Length; i++)
                {
                    Touch currenTouch = Input.touches[i];
                    Vector2 pointPostion = PointToWorldPoint(currenTouch.position);
                    _current[i] = new DataPoint(pointPostion, true);
                }
            }
            else
            {
                IsActivated = false;
            }
        }

        public override DataPoint[] GetInputs()
        {
            return _current;
        }

    }
}