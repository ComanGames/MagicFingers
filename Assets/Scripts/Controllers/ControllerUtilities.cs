using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public static class ControllerUtilities
    {

        public static IController[] ControllerForCurrentPlatform(Camera camera,float zDitance,bool testTouch)
        {
            IController mouseController = new MouseController();
            mouseController.Initialize(camera, zDitance);

            IController andoridController = new TouchAndroidController();
            andoridController.Initialize(camera, zDitance);

            List<IController> controllers =new List<IController>();

            controllers.Add(mouseController);

#if UNITY_ANDROID
            if (testTouch)
            {
                controllers = new List<IController>();
                controllers.Add(andoridController);
            }
#endif

            return controllers.ToArray();
        }
    }
}