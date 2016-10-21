using System;
using System.Collections.Generic;
using GameQuest;
using UnityEngine;

namespace LevelDesignTools
{
    public static class CurveTools
    {
        public static Vector3[] SmoothDots(Vector3[] arrayToCurve, float smoothness)
        {
            List<Vector3> curvedPoints;
            int pointsLength = 0;
            int curvedLength = 0;

            if (smoothness < 1.0f) smoothness = 1.0f;

            pointsLength = arrayToCurve.Length;

            curvedLength = (pointsLength*Mathf.RoundToInt(smoothness)) - 1;
            curvedPoints = new List<Vector3>(curvedLength);

            for (int pointInTimeOnCurve = 0; pointInTimeOnCurve < curvedLength + 1; pointInTimeOnCurve++)
            {
                var t = Mathf.InverseLerp(0, curvedLength, pointInTimeOnCurve);

                var points = new List<Vector3>(arrayToCurve);

                for (int j = pointsLength - 1; j > 0; j--)
                {
                    for (int i = 0; i < j; i++)
                    {
                        points[i] = (1 - t)*points[i] + t*points[i+1];
                    }
                }

                curvedPoints.Add(points[0]);
            }
            return curvedPoints.ToArray();
        }

        public  static Curve MakeSmoothCurve(FingerDot[] FingerDots, float smoothness)
        {
            Vector3[] arrayToCurve = new Vector3[FingerDots.Length];
            for (int i = 0; i < FingerDots.Length; i++)
                arrayToCurve[i] = FingerDots[i].transform.position;

            Vector3[] outPutArr = new Vector3[arrayToCurve.Length];
            Array.Copy(arrayToCurve,outPutArr,arrayToCurve.Length);
            float[] distScale = new float[arrayToCurve.Length];
            Vector3[] distDirection = new Vector3[arrayToCurve.Length];


            for (int j = 0; j < 100; j++)
            {
                Vector3[] pointsOnDistance = SmoothDots(outPutArr, 1f);
                for (int i = 0; i < pointsOnDistance.Length; i++)
                {
                    distDirection[i] = (arrayToCurve[i] - pointsOnDistance[i]).normalized;
                    distScale[i] +=  (Vector3.Distance(arrayToCurve[i], pointsOnDistance[i]) * 2f) - distScale[i];
                    outPutArr[i] = outPutArr[i] + (distDirection[i] * distScale[i]);
                }

            }
            return new Curve(SmoothDots(outPutArr, smoothness));
        }

        public static Curve MakeLine(FingerDot[] fingerDots)
        {
            List<Vector3> dots = new List<Vector3>();
            foreach (FingerDot dot in fingerDots)
                dots.Add(dot.transform.position);

            return new Curve(dots.ToArray());
    }

    }
}