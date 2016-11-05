using System.Collections.Generic;
using PlatfromTools;
using PlatfromTools.Controllers;
using UnityEngine;

namespace GamePlay
{
    public class GameManager : MonoBehaviour
    {
        public FingerPoolManager FingerPool;
        public static GameManager Instance;

        public void Awake()
        {
            Instance = this;
        }


        public void Update()
        {
            ControllerUpdate();
        }

        private void ControllerUpdate()
        {

            AbstractController controller = PlatformUtilities.GetController();
            if (controller != null)
            {
                List<DataPoint> inputs = new List<DataPoint>();

                controller.Update();
                if (controller.IsActive())
                    inputs.AddRange(controller.GetInputs());

                //Collect all touches


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
        }

        private void SetFingerToPoint(Transform activeFinger, Vector2 mousePosition)
        {
            activeFinger.transform.position = new Vector3(mousePosition.x, mousePosition.y, activeFinger.position.z);
        }
    }
}