using System;
using UnityEngine;

namespace LevelDesignTools
{
    [Serializable]
    public class CurveDot
    {
        public Vector3 Point;
        public Vector3 Angle;

        public CurveDot(Vector3 point)
        {
            Point = point;
            Angle = Vector3.zero;
        }

        public CurveDot(Vector3 point, Vector3 angle)
        {
            Point = point;
            Angle = angle;
        }
    }
}