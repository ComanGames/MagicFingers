using UnityEngine;

namespace PlatfromTools.Controllers
{
    public class MouseController : AbstractController 
    {
        private Vector2 _currentPostion;


        public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsActivated = true;
        }
        else if ((!Input.GetMouseButton(0))&& IsActivated)
        {
            IsActivated = false;
        }

        if (IsActivated)
        {
            Vector3 position = Input.mousePosition;
            _currentPostion = PointToWorldPoint(position);
        }
    }


        public override DataPoint[] GetInputs()
        {
            if (IsActivated == false)
                return null;
            return new[] { new DataPoint(_currentPostion,true)};

        }

        public MouseController(Camera camera, float zDistance) : base(camera, zDistance)
        {
        }
    }
}