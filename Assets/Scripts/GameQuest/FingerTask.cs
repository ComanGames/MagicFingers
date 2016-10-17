using System.Collections.Generic;
using UnityEngine;

namespace GameQuest
{
    public class FingerTask : MonoBehaviour
    {
        public FingerDot[] FingerDots;
        private Queue<FingerDot> _dotsQueue;
        private FingerDot _currentDot;

        public void InitTask()
        {
            _dotsQueue = new Queue<FingerDot>(FingerDots);
            StartNextTask();
        }

        public void StartNextTask()
        {
            if(_currentDot!=null)
                _currentDot.Deactivate();

            if (_dotsQueue.Count > 0)
            {
                _currentDot = _dotsQueue.Dequeue();

                _currentDot.Activate();
                _currentDot.CallOnCollistion(StartNextTask);
            }
            else
            {
                Debug.Log("Task");
            }



        }
    }
}