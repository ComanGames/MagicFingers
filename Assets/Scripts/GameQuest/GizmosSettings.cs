using UnityEngine;

namespace GameQuest
{
    public class GizmosSettings : MonoBehaviour
    {
        [Range(0,10)]
        public float DotSize = 5;

        [Range(0,10)]
        public float LineFat = 2;

        [Range(0,100)]
        public float CurveStep =0.1f;

        public AnimationCurve Curve;


        public static float SphereSize { get; set; }
        public static float LineFatSize { get; set; }
        public static float CurveStepSize { get; set; }
        public static AnimationCurve AngleSmooth { get; set; }

        public void OnDrawGizmos()
        {
            SphereSize = DotSize;
            LineFatSize = LineFat;
            CurveStepSize = CurveStep;
            AngleSmooth = Curve;
        }
    }
}