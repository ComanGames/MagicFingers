using System.Collections.Generic;
using Drawing;
using LevelDesignTools;
using UnityEngine;

namespace GameQuest
{
    public class FingerTask : MonoBehaviour
    {
        public Transform ObjectToFallow;
        public TaskGizmo GizmoTask;
        private FingerDot _currentDot;
        public Curve OurCurve;
        public VisualTask Visual;
        private bool _isFallow;
        private int _currentIndex;

        public void Start()
        {
           Visual.SetActiveAt(0,OurCurve.CurveDots[0]); 
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