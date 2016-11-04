using System.Collections.Generic;
using Drawing;
using LevelDesignTools;
using UnityEngine;

namespace GameQuest
{
    public class FingerTask : MonoBehaviour
    {
        public TaskGizmo GizmoTask;
        private FingerDot _currentDot;
        public Curve OurCurve;
        public VisualTask Visual; 


        public void Awake()
        {
        }

        public void InitTask()
        {
            StartNextTask();
        }

        public void OnDrawGizmos()
        {
            OurCurve = GizmoTask.UpdateGizmo(transform);
            Visual.DrawCurve(OurCurve);
            
        }

        public void StartNextTask()
        {
            Queue<FingerDot> dotsQueue = new Queue<FingerDot>();
            if (_currentDot != null)
                _currentDot.Deactivate();

            if (dotsQueue.Count > 0)
            {
                _currentDot = dotsQueue.Dequeue();

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