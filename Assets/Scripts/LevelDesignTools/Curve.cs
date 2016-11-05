using System;
using UnityEngine;

namespace LevelDesignTools
{
    [Serializable]
    public class Curve
    {
        public CurveDot[] CurveDots;

        public Curve(Vector3[] curveDots)
        {
            CurveDot[] finalDots = new CurveDot[curveDots.Length];
            for (int i = 0; i < curveDots.Length-1; i++)
                 finalDots[i] =CurveDotFromDots(curveDots[i], curveDots[i + 1]);
            int count = curveDots.Length;
            finalDots[count-1] = new CurveDot(curveDots[count-1],finalDots[count-2].Angle);
            CurveDots = finalDots;
        }

        private static CurveDot CurveDotFromDots(Vector3 startPoint, Vector3 finalPoint)
        {
            var angle = Vector3.Angle(new Vector3(0, 1, 0), (finalPoint - startPoint).normalized);
            Vector3 direction = new Vector3(0, 0, ((startPoint.x < finalPoint.x) ? 90 - angle : angle + 90));
            CurveDot curveDot = new CurveDot(startPoint, direction);
            return curveDot;
        }
    }
}