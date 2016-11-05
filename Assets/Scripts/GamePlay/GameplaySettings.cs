using System;
using UnityEngine;

namespace GamePlay
{
    public class GameplaySettings : MonoBehaviour
    {
        private static GameplaySettings _instance;
        public Camera GameCamera;

        public static Camera MainCamera
        {
            get { return _instance.GameCamera; }
        }

        public static float CameraZDistance
        {
            get { return MainCamera.transform.position.z*-1f; }
        }


        public void Awake()
        {
            _instance = this;

        }
    }
}