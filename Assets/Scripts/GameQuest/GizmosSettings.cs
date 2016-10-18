using UnityEngine;

namespace GameQuest
{
    public class GizmosSettings : MonoBehaviour
    {
        [Range(0,10)]
        public float DotSize = 5;

        [Range(0,10)]
        public float LineFat = 2;

        [Range(0,1)]
        public float CurveStep =0.1f;

        public AnimationCurve AngleCure;

        public static AnimationCurve LineCurve { get; set; }
        public static float SphereSize { get; set; }
        public static float LineFatSize { get; set; }
        public static float CurveStepSize { get; set; }

        public void OnDrawGizmos()
        {
            SphereSize = DotSize;
            LineFatSize = LineFat;
            LineCurve = AngleCure;
            CurveStepSize = CurveStep;
        }
    }
}