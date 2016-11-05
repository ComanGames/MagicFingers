using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class FingerPoolManager : MonoBehaviour
    {
        public GameObject[] Fingers;
        private List<GameObject> _activeFingers;

        public void Start()
        {
            _activeFingers = new List<GameObject>();
            _sleepingFingers = new List<GameObject>();
            foreach (GameObject finger in Fingers)
            {
                finger.SetActive(false);
                _sleepingFingers.Add(finger);

            }
        }

        private List<GameObject> _sleepingFingers;

        public void AllToSleep()
        {
            while (_activeFingers.Count > 0)
                ReturnFinger(_activeFingers[_activeFingers.Count-1]);
        }
        public Transform[] GetFingerCount(int count)

        {
            if (count <=0)
                throw  new ArgumentException("I can't give you zero fingers");

            if (count > _activeFingers.Count + _sleepingFingers.Count)
                throw  new ArgumentException("You require to much fingers");

            while (_activeFingers.Count < count)
                GetFinger();
            while (_activeFingers.Count>count)
                ReturnFinger(_activeFingers[_activeFingers.Count-1]);
            Transform[] result = new Transform[count];
            for (int i = 0; i < result.Length; i++)
                result[i] = _activeFingers[i].transform;

            return result;
        }
        public void ReturnFinger(GameObject finger)
        {
            if (finger != null&&_activeFingers.Contains(finger))
            {
                _activeFingers.Remove(finger);
                finger.SetActive(false);
                _sleepingFingers.Add(finger);
            }
            else
            {
               throw  new  ArgumentException("Finger is null. You can't return null finger");
            }

        }

        public Transform GetFinger()
        {
            Transform result = null;
            if (_sleepingFingers != null && _sleepingFingers.Count > 0)
            {
                int lastIndex = _sleepingFingers.Count - 1;
                result = _sleepingFingers[lastIndex].transform;
                result.gameObject.SetActive(true);
                _activeFingers.Add(result.gameObject);
                _sleepingFingers.RemoveAt(lastIndex);
            }

            return result;
        }
    }
}
