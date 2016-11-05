using GamePlay;
using UnityEngine;

namespace PlatfromTools.Controllers
{
    public abstract class AbstractController
    {
        protected bool IsActivated;

        public abstract void Update();

        public abstract DataPoint[] GetInputs();

         public virtual bool IsActive()
        {
            //Todo: Controller update in is active cheeking
            Update();
            return IsActivated;
        }

        protected virtual Vector3 PointToWorldPoint(Vector3 position)
        {
            Camera camera = GameplaySettings.MainCamera;
            float distance = GameplaySettings.CameraZDistance;
            return camera.ScreenToWorldPoint(position + (Vector3.forward*distance));
        }
    }
}