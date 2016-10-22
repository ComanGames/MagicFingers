using System;
using LevelDesignTools;
using UnityEngine;

namespace GameQuest
{
    public class FingerDot : MonoBehaviour
    {
        public Transform LeftDot;
        public Transform RightDot;
        public bool IsActive { get; set; }
        private Action _actionToCall;

        public void Start()
        {
            Deactivate();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "finger" && _actionToCall != null)
            {
                _actionToCall();
                _actionToCall = null;
            }
        }

        public void OnDrawGizmos()
        {
        }

        public void CallOnCollistion(Action actionToDo)
        {
            _actionToCall = actionToDo;
        }
        public void Activate()
        {
            IsActive = true;
            gameObject.SetActive(IsActive);
        }
        public void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(IsActive);
        }

    }
}