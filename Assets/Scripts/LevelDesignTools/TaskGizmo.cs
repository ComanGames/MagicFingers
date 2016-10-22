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
        private CurveType _prevCurveType = CurveType.Line;
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
                Gizmos.DrawSphere(fingerDot.transform.position, GizmosSettings.SphereSize);

                if (fingerDot.LeftDot != null && TypeOfCurve == CurveType.ThirdDot)
                    DrawExtrDots(fingerDot.LeftDot, fingerDot.transform);

                if (fingerDot.RightDot != null && TypeOfCurve == CurveType.ThirdDot)
                    DrawExtrDots(fingerDot.RightDot, fingerDot.transform);
            }
        }

        private static void DrawExtrDots(Transform extraDot, Transform transform)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawCube(extraDot.position, Vector3.one*GizmosSettings.CubeSize);
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
            if (curve != null && curve.CurveDots != null)
                _transform.GetComponent<MeshFilter>().mesh = MeshGenerator.MeshFromCurve(curve);
            return curve;
        }


        public void AutoGetDots()
        {
            if (_fingerDots == null)
                _childCount = -1;
            if (_prevCurveType != TypeOfCurve)
            {
                WarkWithExtraDots();
                _prevCurveType = TypeOfCurve;
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
                WarkWithExtraDots();
            }
        }

        private void WarkWithExtraDots()
        {
            if (_prevCurveType == CurveType.ThirdDot)
            {
                //Cleaning
                foreach (FingerDot fingerDot in _fingerDots)
                {
                    if (fingerDot.LeftDot != null)
                        Object.DestroyImmediate(fingerDot.LeftDot.gameObject);
                    if (fingerDot.RightDot != null)
                        Object.DestroyImmediate(fingerDot.RightDot.gameObject);
                }
            }
            if (TypeOfCurve == CurveType.ThirdDot)
            {
                if (_fingerDots != null)
                {
                    GameObject leftDot;
                    GameObject rightDot;

                    rightDot = CreateDot("Right");
                    rightDot.transform.position = Vector3.Lerp(_fingerDots[0].transform.position,
                        _fingerDots[1].transform.position, 0.5f);
                    rightDot.transform.parent = _fingerDots[0].transform;
                    _fingerDots[0].RightDot = rightDot.transform;

                    for (int f = 1; f < _fingerDots.Length - 1; f++)
                    {
                        leftDot = CreateDot("Left");
                        rightDot = CreateDot("Right");

                        leftDot.transform.position = Vector3.Lerp(_fingerDots[f - 1].transform.position,
                            _fingerDots[f].transform.position, 0.5f);
                        rightDot.transform.position = Vector3.Lerp(_fingerDots[f].transform.position,
                            _fingerDots[f + 1].transform.position, 0.5f);

                        leftDot.transform.parent = _fingerDots[f].transform;
                        rightDot.transform.parent = _fingerDots[f].transform;

                        _fingerDots[f].LeftDot = leftDot.transform;
                        _fingerDots[f].RightDot = rightDot.transform;
                    }
                    int i = _fingerDots.Length - 1;
                    leftDot = CreateDot("Left");

                    leftDot.transform.position = Vector3.Lerp(_fingerDots[i - 1].transform.position,
                        _fingerDots[i].transform.position, 0.5f);

                    leftDot.transform.parent = _fingerDots[i].transform;

                    _fingerDots[i].LeftDot = leftDot.transform;
                }
            }
        }

        private GameObject CreateDot(string name)
        {
            GameObject result = new GameObject();
            result.name = name + " Dot";
            return result;
        }
    }
}