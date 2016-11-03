using PlatfromTools.Controllers;
using UnityEngine;

namespace PlatfromTools
{
    public static class PlatformUtilities
    {

        public static AbstractController GetController(Camera camera,float zDitance)
        {
            AbstractController mouseController = new MouseController(camera,zDitance);

            AbstractController andoridController = new AndroidController(camera,zDitance);

            return mouseController;
        }
    }
}