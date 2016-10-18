using System.Collections.Generic;
using UnityEngine;

namespace GameQuest
{
    public class FingerTask : MonoBehaviour
    {
        public bool UpdateCure;
        public AnimationCurve realCurve;
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
            Vector3[] dots= new Vector3[FingerDots.Length];
            for (int i = 0; i < FingerDots.Length; i++)
                dots[i] = FingerDots[i].transform.position;
             AnimationCurve curve = SmoothDots(dots);
            if (UpdateCure)
                realCurve = curve;
            float realLength = DotLineLength(dots);
            Vector3[] curvedDots = DotsFromCurve(curve,dots ,realLength , GizmosSettings.CurveStepSize);
            for (int i = 1; i < curvedDots.Length; i++)
               Debug.DrawLine(curvedDots[i-1],curvedDots[i]); 
            
            Mesh mesh = MeshGenerator.LinesFromDots(dots);
            mesh.name = "MeshFromLine - " + mesh.GetHashCode();
            if (GetComponent<MeshFilter>() == null)
                gameObject.AddComponent<MeshFilter>();
            MeshFilter meshFilter = gameObject.GetComponent < MeshFilter>();
            meshFilter.mesh = mesh;

        }

        private Vector3[] DotsFromCurve(AnimationCurve curve,Vector3[] realDots,float totalLength, float step)
        {
            int dotsCount = (int) (totalLength/step) + 1;
            float realDotsDistance = totalLength/(float) dotsCount;
            Vector3[] result= new Vector3[dotsCount];

            float currentDistance = 0;
            int realDotIndex =0;
            float pastDistance = 0;
            for (int i = 0; i < result.Length-1; i++)
            {
                float lerp = curve.Evaluate(currentDistance);
                Debug.Log("curve[realDotIndex].time =" + curve[realDotIndex].time + " Current distance"+currentDistance);
                

                if (curve[realDotIndex].time>currentDistance)
                {
                    realDotIndex++;
                    pastDistance = currentDistance;
                    Debug.Log("Real dot index ="+realDotIndex +"currentDistance" +currentDistance);
                }
                float lerpX = (currentDistance - pastDistance)/(Vector3.Distance(realDots[realDotIndex],realDots[realDotIndex+1]) );
                float x = Mathf.Lerp(realDots[realDotIndex].x, realDots[realDotIndex + 1].x,lerpX);
                float y = Mathf.Lerp(realDots[realDotIndex].y, realDots[realDotIndex + 1].y,lerp%1f);
                result[i] = new Vector3(x,y,0);
                currentDistance += realDotsDistance;
            }

            result[result.Length - 1] = realDots[realDots.Length - 1];

            return result;
        }

        private AnimationCurve SmoothDots(Vector3[] dots)
        {
            AnimationCurve anim = new AnimationCurve();
            float outTangent = GizmosSettings.LineCurve.keys[0].outTangent;
            float inTangent = GizmosSettings.LineCurve.keys[1].inTangent;
            anim.AddKey(new Keyframe(0,0,inTangent,0));

            float totalLength  = DotLineLength(dots);

            float totalTime = 0;
            for (int i = 1; i < dots.Length; i++)
            {
                totalTime += (Vector3.Distance(dots[i - 1], dots[i]));
                float time = totalTime;
                float height = (float)i;
                anim.AddKey(new Keyframe(time,height,inTangent,outTangent));
            }

            return anim;
        }

        private static float DotLineLength(Vector3[] dots)
        {
            float totalLength =0;
            for (int i = 1; i < dots.Length; i++)
                totalLength += Vector3.Distance(dots[i - 1], dots[i]);
            return totalLength;
        }

        private void AutoGetDots()
        {
            if (_childCount!=transform.childCount)
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