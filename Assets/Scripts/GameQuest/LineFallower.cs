using LevelDesignTools;
using UnityEngine;

namespace GameQuest
{
    public class LineFallower : MonoBehaviour
    {
        public float Speed;
        public FingerTask Task;
        private Curve _currentCurve;
        private int _currentPostion;
        private float _distance;
        public void Start()
        {
            _currentCurve = Task.OurCurve;
            ResetTeask();
        }

        private void ResetTeask()
        {
            _currentPostion = 0;
            transform.position = _currentCurve.CurveDots[_currentPostion].Point;
            transform.rotation = Quaternion.Euler(_currentCurve.CurveDots[_currentPostion].Angle);
        }

        public void Update()
        {

            _distance += Speed*Time.deltaTime;
            if (_distance > 1.0f)
            {
                _distance = _distance%1f;
                _currentPostion++;
                if (_currentPostion + 1 == _currentCurve.CurveDots.Length)
                    ResetTeask();
            }
            transform.position = Vector3.Lerp(_currentCurve.CurveDots[_currentPostion].Point, _currentCurve.CurveDots[_currentPostion + 1].Point, _distance);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(_currentCurve.CurveDots[_currentPostion].Angle,_currentCurve.CurveDots[_currentPostion+1].Angle,_distance));
        }
    }
}