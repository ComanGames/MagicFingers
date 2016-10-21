using System.Collections.Generic;
using LevelDesignTools;
using UnityEngine;

namespace GameQuest
{
    public class FingerTask : MonoBehaviour
    {
        public bool UpdateCure;
        public FingerDot[] FingerDots;
        private Queue<FingerDot> _dotsQueue;
        private FingerDot _currentDot;
        private int _childCount;

        public void InitTask()
        {
            _dotsQueue = new Queue<FingerDot>(FingerDots);
            StartNextTask();
        }

        public void DrawObject()
        {
            Vector3[] dots = new Vector3[FingerDots.Length];
            for (int i = 0; i < FingerDots.Length; i++)
                dots[i] = FingerDots[i].transform.position;

            Vector3[] curvedDots = CurveTools.MakeSmoothCurve(dots,GizmosSettings.CurveStepSize);

             GetComponent<MeshFilter>().mesh = MeshGenerator.MeshFromCurve(curvedDots);
        }


        private void AutoGetDots()
        {
            if (_childCount != transform.childCount)
            {
                _childCount = transform.childCount;
                List<FingerDot> childDots = new List<FingerDot>();
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    FingerDot fingerDot = child.GetComponent<FingerDot>();
                    if (fingerDot != null)
                        childDots.Add(fingerDot);
                }
                FingerDots = childDots.ToArray();
            }
        }

        public void OnDrawGizmos()
        {
            AutoGetDots();
            DrawObject();
        }

        public void StartNextTask()
        {
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