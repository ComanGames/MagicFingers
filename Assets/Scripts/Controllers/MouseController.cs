using UnityEngine;

namespace Controllers
{
    public class MouseController : BaseController 
    {
        private Vector2 _currentPostion;

        public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isActive = true;
        }
        else if ((!Input.GetMouseButton(0))&& _isActive)
        {
            _isActive = false;
        }

        if (_isActive)
        {
            Vector3 position = Input.mousePosition;
            _currentPostion = PointToWorldPoint(position);
        }
    }


        public override DataPoint[] GetInputs()
        {
            if (_isActive == false)
                return null;
            return new[] { new DataPoint(_currentPostion,true)};

        }
    }
}