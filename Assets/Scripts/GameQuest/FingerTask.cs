using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameQuest
{
    public class FingerTask : MonoBehaviour
    {
        public bool UpdateCure;
        public FingerDot[] FingerDots;
        private Queue<FingerDot> _dotsQueue;
        private FingerDot _currentDot;
        private int _childCount;

        public void InitTask()
        {
            _dotsQueue = new Queue<FingerDot>(FingerDots);
            StartNextTask();
        }

        private void DrawObject()
        {
            Vector3[] dots = new Vector3[FingerDots.Length];
            for (int i = 0; i < FingerDots.Length; i++)
                dots[i] = FingerDots[i].transform.position;
            float realLength = DotLineLength(dots);

            Vector3[] curvedDots = MakeSmoothCurve(dots,GizmosSettings.CurveStepSize);

            Mesh mesh = MeshGenerator.LinesFromDots(curvedDots);
            mesh.name = "MeshFromLine - " + mesh.GetHashCode();
            if (GetComponent<MeshFilter>() == null)
                gameObject.AddComponent<MeshFilter>();
            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            meshFilter.mesh = mesh;

        }

        private Vector3[] DotsFromCurve( Vector3[] realDots, float totalLength, float step)
        {
            int dotsCount = (int)(totalLength / step) + 1;
            float realDotsDistance = totalLength / (float)dotsCount;
            List<Vector3> result = new List<Vector3>();

            float currentDistance = 0;
            int realDotIndex = 0;
            float pastDistance = 0;
            while (currentDistance < totalLength)
            {
                float distance = Vector3.Distance(realDots[realDotIndex], realDots[realDotIndex + 1]);

                if (currentDistance - pastDistance > distance)
                {
                    realDotIndex++;
                    pastDistance = currentDistance;
                    continue;
                }

            float lerpX = (currentDistance - pastDistance)/ distance;


                float x = Mathf.Lerp(realDots[realDotIndex].x, realDots[realDotIndex + 1].x, lerpX);
                float y=0;
                y = Mathf.Lerp(realDots[realDotIndex].y, realDots[realDotIndex + 1].y, GizmosSettings.AngleSmooth.Evaluate(lerpX/2f)*2);

                result.Add(new Vector3(x, y, 0));
                currentDistance += realDotsDistance;

            }

            result.Add(realDots[realDots.Length - 1]);

            return result.ToArray();
        }

        private static float Tang(Vector3 dot1, Vector3 dot2)
        {
            float tang;
            float angle = Vector3.Angle(dot1, dot2);
            tang = Mathf.Tan(angle);
            return tang;
        }

        private static float DotLineLength(Vector3[] dots)
        {
            float totalLength = 0;
            for (int i = 1; i < dots.Length; i++)
                totalLength += Vector3.Distance(dots[i - 1], dots[i]);
            return totalLength;
        }

        private void AutoGetDots()
        {
            if (_childCount != transform.childCount)
            {
                _childCount = transform.childCount;
                List<FingerDot> childDots = new List<FingerDot>();
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    FingerDot fingerDot = child.GetComponent<FingerDot>();
                    if (fingerDot != null)
                        childDots.Add(fingerDot);
                }
                FingerDots = childDots.ToArray();
            }
        }
        public  Vector3[] MakeSmoothCurve(Vector3[] arrayToCurve, float smoothness)
        {
            
            Vector3[] outPutArr = new Vector3[arrayToCurve.Length];
            Array.Copy(arrayToCurve,outPutArr,arrayToCurve.Length);
            float[] distScale = new float[arrayToCurve.Length];
            Vector3[] distDirection = new Vector3[arrayToCurve.Length];
            Vector3[] pointsOnDistanceFirst = SmoothDots(outPutArr, 1f);


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
            return SmoothDots(outPutArr, smoothness);
//            return ();
        }

        private static Vector3[] SmoothDots(Vector3[] arrayToCurve, float smoothness)
        {
            List<Vector3> points;
            List<Vector3> curvedPoints;
            int pointsLength = 0;
            int curvedLength = 0;

            if (smoothness < 1.0f) smoothness = 1.0f;

            pointsLength = arrayToCurve.Length;

            curvedLength = (pointsLength*Mathf.RoundToInt(smoothness)) - 1;
            curvedPoints = new List<Vector3>(curvedLength);

            float t = 0.0f;
            for (int pointInTimeOnCurve = 0; pointInTimeOnCurve < curvedLength + 1; pointInTimeOnCurve++)
            {
                t = Mathf.InverseLerp(0, curvedLength, pointInTimeOnCurve);

                points = new List<Vector3>(arrayToCurve);

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

        public void OnDrawGizmos()
        {
            AutoGetDots();
            DrawObject();
        }

        public void StartNextTask()
        {
            if (_currentDot != null)
                _currentDot.Deactivate();

            if (_dotsQueue.Count > 0)
            {
                _currentDot = _dotsQueue.Dequeue();

                _currentDot.Activate();
                _currentDot.CallOnCollistion(StartNextTask);
            }
            else
            {
                Debug.Log("Task Is Done");
            }



        }
    }
}