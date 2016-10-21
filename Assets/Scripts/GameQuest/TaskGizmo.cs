using System;
using System.Collections.Generic;
using LevelDesignTools;
using UnityEngine;

namespace GameQuest
{
    [Serializable]
    public class TaskGizmo
    {

        public CurveType TypeOfCurve;
        private int _childCount;
        private Transform _transform;
        private FingerDot[] _fingerDots;
        public void UpdateGizmo(Transform original)
        {
            _transform = original;
            AutoGetDots();
            DrawObject();
        }
        public void DrawObject()
        {
            Curve curve = null;
            switch (TypeOfCurve)
            {
                   case CurveType.Auto:
                        curve= CurveTools.MakeSmoothCurve(_fingerDots,GizmosSettings.CurveStepSize);
                    break;    
                   case CurveType.Line:
                        curve= CurveTools.MakeLine(_fingerDots);
                    break;    
            }
            if(curve!=null&&curve.CurveDots!=null)
                _transform.GetComponent<MeshFilter>().mesh = MeshGenerator.MeshFromCurve(curve);
        }


        public void AutoGetDots()
        {
            if (_fingerDots == null)
            {
                _childCount = -1;
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
                        childDots.Add(fingerDot);
                }
                _fingerDots = childDots.ToArray();
            }
        }
    }
}