using UnityEngine;

namespace PlatfromTools.Controllers
{
    public class MouseController : AbstractController 
    {
        private Vector2 _currentPostion;


        public override void Update()
        {
            Debug.Log("We update mouse data");
        if (Input.GetMouseButtonDown(0))
        {
            IsActivated = true;
                Debug.Log("Mouse is pressed down");

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
            return new[] { new DataPoint(_currentPostion,true)};

        }

    }
}