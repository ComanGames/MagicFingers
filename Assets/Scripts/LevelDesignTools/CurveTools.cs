using System;
using System.Collections.Generic;
using System.Linq;
using GameQuest;
using UnityEngine;

namespace LevelDesignTools
{
    public static class CurveTools
    {
        public static Vector3[] SmoothDots(Vector3[] arrayToCurve, float smoothness)
        {
            List<Vector3> curvedPoints;

            if (smoothness < 1.0f) smoothness = 1.0f;

            var pointsLength = arrayToCurve.Length;

            var curvedLength = (pointsLength*Mathf.RoundToInt(smoothness)) - 1;
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

        public  static Curve MakeSmoothCurve(FingerDot[] fingerDots, float smoothness)
        {
            var arrayToCurve = GetVectorFromPoints(fingerDots);

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

        private static Vector3[] GetVectorFromPoints(FingerDot[] fingerDots)
        {
            var arrayToCurve = new Vector3[fingerDots.Length];
            for (int i = 0; i < fingerDots.Length; i++)
                arrayToCurve[i] = fingerDots[i].transform.position - fingerDots[i].transform.parent.position;
            return arrayToCurve;
        }

        public static Curve MakeLine(FingerDot[] fingerDots)
        {
            Vector3[] points = GetVectorFromPoints(fingerDots);

            return new Curve(points);
    }

        public static Curve MakeThirdDotLine(FingerDot[] fingerDots,float smoothes)
        {
            List<Vector3> totalPoints = new List<Vector3>();
            for (int i = 0; i < fingerDots.Length-1; i++)
            {
                var dotsToSmooth = new List<Vector3>();
                FingerDot left = fingerDots[i];
                FingerDot right = fingerDots[i+1];

                dotsToSmooth.Add(left.transform.position - left.transform.parent.position);
                if (left.RightDot != null)
                    dotsToSmooth.Add(left.RightDot.position - left.transform.parent.position);

                if (right.LeftDot != null)
                    dotsToSmooth.Add(right.LeftDot.position - right.transform.parent.position);

                dotsToSmooth.Add(right.transform.position - right.transform.parent.position);
                List<Vector3> smoothDots = SmoothDots(dotsToSmooth.ToArray(),smoothes).ToList();
                smoothDots.RemoveAt(smoothDots.Count-1);
                totalPoints.AddRange(smoothDots);
            }
            totalPoints.Add(fingerDots[fingerDots.Length-1].transform.position - fingerDots[fingerDots.Length-1].transform.parent.position);
            return new Curve(totalPoints.ToArray());
        }
    }
}