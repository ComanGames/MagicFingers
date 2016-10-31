using UnityEngine;

namespace Controllers
{
    public class BaseController:IController
    {
        protected bool _isActive;
        protected Camera _currentCamera;
        protected float _zDistance;
        public void Initialize(Camera main,float zDistance) 
        {
            _currentCamera = main;
            _zDistance = zDistance;

        }
        public virtual void Update()
        {
            throw new System.NotImplementedException();
        }

        public virtual DataPoint[] GetInputs()
        {
            throw new System.NotImplementedException();
        }

        public bool IsActive()
        {
            return _isActive;
        }

        protected Vector3 PointToWorldPoint(Vector3 position)
        {
            return _currentCamera.ScreenToWorldPoint(position + (Vector3.forward*_zDistance));
        }
    }
}