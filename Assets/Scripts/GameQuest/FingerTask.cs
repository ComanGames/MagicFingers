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
        public float DistanceToStart = 1f;
        private bool _isFallow;
        private int _currentIndex;

        public void Start()
        {
           Visual.SetActiveAt(0,OurCurve.CurveDots[0]);
            _isFallow = false;
            _currentIndex = 0;
        }

        public void Update()
        {
            if(ObjectToFallow.gameObject.activeSelf&&_currentIndex<OurCurve.CurveDots.Length)

                if (!_isFallow)
                {
                    float distance = Vector3.Distance(ObjectToFallow.position, OurCurve.CurveDots[0].Point);
                    Debug.Log("Distance to start"+distance);
                    if (distance < DistanceToStart)
                    {
                        _isFallow = true;
                        _currentIndex = 1;
                    }
                }
                else
                {
                    float current = Vector3.Distance(ObjectToFallow.position, OurCurve.CurveDots[_currentIndex-1].Point);
                    float next = Vector3.Distance(ObjectToFallow.position, OurCurve.CurveDots[_currentIndex].Point);
                    if (current>next)
                    {
                        _currentIndex++;
                        Visual.SetActiveAt(_currentIndex,OurCurve.CurveDots[_currentIndex-1]);
                    }
                }
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