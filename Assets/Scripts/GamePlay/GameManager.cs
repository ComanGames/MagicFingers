using System.Collections.Generic;
using PlatfromTools;
using PlatfromTools.Controllers;
using UnityEngine;

namespace GamePlay
{
    public class GameManager:MonoBehaviour
    {
        public FingerPoolManager FingerPool;
        public Camera MainCamera;
        public float ZDistance;
        private AbstractController _currentContrllers;

        public void Start()
        {
            _currentContrllers = PlatformUtilities.GetController(MainCamera, ZDistance);
        }

        public void Update()
        {
            ControllerUpdate();
        }

        private void ControllerUpdate()
        {
            List<DataPoint> inputs = new List<DataPoint>();
            //Collect all touches
                _currentContrllers.Update();
                if (_currentContrllers.IsActive())
                    inputs.AddRange(_currentContrllers.GetInputs());

            //Making fingers for all touches
            if (inputs.Count > 0)
            {
                Transform[] fingers = FingerPool.GetFingerCount(inputs.Count);
                for (int i = 0; i < inputs.Count; i++)
                    SetFingerToPoint(fingers[i], inputs[i].Postion);
            }
            else
                FingerPool.AllToSleep();
        }

        private void SetFingerToPoint(Transform activeFinger, Vector2 mousePosition)
        {
            activeFinger.transform.position = new Vector3(mousePosition.x,mousePosition.y,activeFinger.position.z);
        }
    }
}