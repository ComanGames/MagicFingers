using System;
using System.Collections.Generic;
using GameQuest;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LevelDesignTools
{
    [Serializable]
    public class TaskGizmo
    {

        public CurveType TypeOfCurve;
        private CurveType _previousCurveType = CurveType.Line;
        private int _childCount;
        private Transform _transform;
        private FingerDot[] _fingerDots;

        public Curve UpdateGizmo(Transform original)
        {
            _transform = original;
            AutoGetDots();
            DrawGizmos();
            return DrawObject();
            
        }

        public void DrawGizmos()
        {
            if (_fingerDots == null)
                return;

            foreach (FingerDot fingerDot in _fingerDots)
            {

#pragma warning disable 618
                if (fingerDot.LeftDot != null && TypeOfCurve == CurveType.ThirdDot&&fingerDot.LeftDot.gameObject.active)
                    DrawExtrDots(fingerDot.LeftDot, fingerDot.transform);

                if (fingerDot.RightDot != null && TypeOfCurve == CurveType.ThirdDot&&fingerDot.RightDot.gameObject.active)
                    DrawExtrDots(fingerDot.RightDot, fingerDot.transform);
#pragma warning restore 618
            }
        }

        private static void DrawExtrDots(Transform extraDot, Transform transform)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, extraDot.position);
            Gizmos.color = Color.white;
        }

        public Curve DrawObject()
        {
            Curve curve = null;
            switch (TypeOfCurve)
            {
                case CurveType.Auto:
                    curve = CurveTools.MakeSmoothCurve(_fingerDots, GizmosSettings.CurveStepSize);
                    break;
                case CurveType.Line:
                    curve = CurveTools.MakeLine(_fingerDots);
                    break;
                case CurveType.ThirdDot:
                    curve = CurveTools.MakeThirdDotLine(_fingerDots,GizmosSettings.CurveStepSize);
                    break;
            }
            return curve;
        }


        public void AutoGetDots()
        {
            if (_fingerDots == null)
                _childCount = -1;
            if (_previousCurveType != TypeOfCurve)
            {
                WorkWithExtraDots();
                _previousCurveType = TypeOfCurve;
            }

            if (_childCount != _transform.childCount)
            {
                _childCount = _transform.childCount;
                List<FingerDot> childDots = new List<FingerDot>();
                for (int i = 0; i < _transform.childCount; i++)
                {
                    Transform child = _transform.GetChild(i);
                    FingerDot fingerDot = child.GetComponent<FingerDot>();
                    if (fingerDot != null)
                    {
                        childDots.Add(fingerDot);
                    }
                }
                _fingerDots = childDots.ToArray();
                WorkWithExtraDots();
            }
        }

        private void WorkWithExtraDots()
        {
            if (_previousCurveType == CurveType.ThirdDot)
            {
                //Cleaning
                foreach (FingerDot fingerDot in _fingerDots)
                {
                    if (fingerDot.LeftDot != null)
                        fingerDot.LeftDot.gameObject.SetActive(false);
                    if (fingerDot.RightDot != null)
                        fingerDot.RightDot.gameObject.SetActive(false);
                }
            }
            if (TypeOfCurve == CurveType.ThirdDot)
            {
                if (_fingerDots != null)
                {
                    GameObject leftDot;
                    GameObject rightDot;

                    //Right dot
                    if (_fingerDots[0].RightDot == null)
                        rightDot = CreateDot("Right");
                    else
                        rightDot = _fingerDots[0].RightDot.gameObject;

                    rightDot.SetActive(true);

                    rightDot.transform.position = Vector3.Lerp(_fingerDots[0].transform.position,
                        _fingerDots[1].transform.position, 0.5f);
                    rightDot.transform.parent = _fingerDots[0].transform;
                    _fingerDots[0].RightDot = rightDot.transform;

                    for (int f = 1; f < _fingerDots.Length - 1; f++)
                    {
                        //Left dot
                        if (_fingerDots[f].LeftDot == null)
                        {
                            leftDot = CreateDot("Left");
                            leftDot.transform.position = Vector3.Lerp(_fingerDots[f - 1].transform.position, _fingerDots[f].transform.position, 0.5f);
                            leftDot.transform.parent = _fingerDots[f].transform;
                            _fingerDots[f].LeftDot = leftDot.transform;
                        }
                        else
                        {
                            leftDot = _fingerDots[f].LeftDot.gameObject;
                            leftDot.SetActive(true);
                        }
                        //Right dot
                        if (_fingerDots[f].RightDot == null)
                        {
                            rightDot = CreateDot("Right");
                            rightDot.transform.position = Vector3.Lerp(_fingerDots[f].transform.position, _fingerDots[f + 1].transform.position, 0.5f);
                            rightDot.transform.parent = _fingerDots[f].transform;
                            _fingerDots[f].RightDot = rightDot.transform;
                        }
                        else
                        {
                            rightDot = _fingerDots[f].RightDot.gameObject;
                            rightDot.SetActive(true);
                        }



                    }
                    int i = _fingerDots.Length - 1;
                    //Left dot
                    if (_fingerDots[i].LeftDot == null)
                        leftDot = CreateDot("Left");
                    else
                        leftDot = _fingerDots[i].LeftDot.gameObject;

                    leftDot.SetActive(true);


                    leftDot.transform.position = Vector3.Lerp(_fingerDots[i - 1].transform.position, _fingerDots[i].transform.position, 0.5f);

                    leftDot.transform.parent = _fingerDots[i].transform;

                    _fingerDots[i].LeftDot = leftDot.transform;
                }
            }
        }

        private GameObject CreateDot(string name)
        {
            GameObject result = Object.Instantiate(GizmosSettings.CurveDotInstance);
            result.name = name + " Dot";
            return result;
        }
    }
}