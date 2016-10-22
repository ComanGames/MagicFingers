using System.Collections.Generic;
using LevelDesignTools;
using UnityEngine;

namespace GameQuest
{
    public class FingerTask : MonoBehaviour
    {
        public TaskGizmo GizmoTask;
        private FingerDot _currentDot;
        public Curve OurCurve;
        public bool IsDrawing;

        public void InitTask()
        {
            StartNextTask();
        }

        public void OnDrawGizmos()
        {
            if (IsDrawing) 
                OurCurve = GizmoTask.UpdateGizmo(transform);
        }

        public void StartNextTask()
        {
            Queue<FingerDot> _dotsQueue = new Queue<FingerDot>();
            if (_currentDot != null)
                _currentDot.Deactivate();

            if (_dotsQueue.Count > 0)
            {
                _currentDot = _dotsQueue.Dequeue();

                _currentDot.Activate();
                _currentDot.CallOnCollistion(StartNextTask);
            }
            else
            {
                Debug.Log("Task Is Done");
            }



        }
    }
}