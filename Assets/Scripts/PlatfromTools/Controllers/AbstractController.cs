using UnityEngine;

namespace PlatfromTools.Controllers
{
    public abstract class AbstractController
    {
        protected bool IsActivated;
        protected Camera CurrentCamera;
        protected float ZDistance;

        protected AbstractController(Camera camera,float zDistance) 
        {
            CurrentCamera = camera;
            ZDistance = zDistance;

        }
        public abstract void Update();

        public abstract DataPoint[] GetInputs();

         public virtual bool IsActive()
        {
            return IsActivated;
        }

        protected virtual Vector3 PointToWorldPoint(Vector3 position)
        {
            return CurrentCamera.ScreenToWorldPoint(position + (Vector3.forward*ZDistance));
        }
    }
}