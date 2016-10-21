using UnityEngine;

namespace LevelDesignTools
{
    public class GizmosSettings : MonoBehaviour
    {
        [Range(0,10)]
        public float DotSize = 5;

        [Range(0,10)]
        public float LineFat = 2;

        [Range(0,100)]
        public float CurveStep =0.1f;

        [Range(0,100)]
        public int CoinCount = 10;

        public AnimationCurve Curve;


        public static float SphereSize { get; set; }
        public static float LineFatSize { get; set; }
        public static float CurveStepSize { get; set; }
        public static int CoinCountSize { get; set; }

        public static AnimationCurve AngleSmooth { get; set; }

        public void OnDrawGizmos()
        {
            SphereSize = DotSize;
            LineFatSize = LineFat;
            CurveStepSize = CurveStep;
            CoinCountSize = CoinCount;
            AngleSmooth = Curve;
        }
    }
}