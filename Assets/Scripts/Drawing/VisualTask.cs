using System.Collections.Generic;
using LevelDesignTools;
using UnityEngine;

namespace Drawing
{
    public class VisualTask : MonoBehaviour
    {
        public GameObject StartPoint;
        public GameObject EndPoint;
        public GameObject Line;
        public GameObject OutLine;
        public Material ColorMaterial;

        public void Start()
        {

        }

        public void DrawCurve(Curve curve)
        {
            if (curve != null && curve.CurveDots != null)
            {


                int last = curve.CurveDots.Length - 1;

                MeshFilter outlineMesh = OutLine.GetComponent<MeshFilter>();
                MeshFilter startPointMesh = StartPoint.GetComponent<MeshFilter>();
                MeshFilter endPointMesh = EndPoint.GetComponent<MeshFilter>();
                MeshFilter lineMesh = Line.GetComponent<MeshFilter>();

                // Mesh generation
                lineMesh.mesh = MeshGenerator.MeshFromCurve(curve);
                startPointMesh.mesh = MeshGenerator.CircleMesh(GizmosSettings.LineFatSize/2f,180,GizmosSettings.CircleStepSize);
                endPointMesh.mesh = MeshGenerator.CircleMesh(GizmosSettings.LineFatSize/2f,180,GizmosSettings.CircleStepSize);

                Vector3 startPointAngle = (curve.CurveDots[0].Angle + Vector3.forward*90);
                Vector3 endPointAngle = curve.CurveDots[last].Angle - Vector3.forward*90;

                //Circle locations
                StartPoint.transform.localPosition = curve.CurveDots[0].Point;
                StartPoint.transform.eulerAngles =startPointAngle;

                EndPoint.transform.localPosition = curve.CurveDots[last].Point;
                EndPoint.transform.eulerAngles =endPointAngle;

                //

                float outLineDistance = (GizmosSettings.LineFatSize +GizmosSettings.OutLineFatSize)/2.0f;

                List<Vector3> outlineDots = new List<Vector3>(); 
                outlineDots.AddRange(OutLineCurveLeft(curve,outLineDistance));

                outlineDots.AddRange(OutLineCircle(StartPoint.transform.position,outLineDistance, 180, GizmosSettings.CircleStepSize));

                foreach (Vector3 pointVector3 in outlineDots)
                    Gizmos.DrawCube(pointVector3, Vector3.one);

                OutLine.GetComponent<MeshRenderer>().material = ColorMaterial;
                StartPoint.GetComponent<MeshRenderer>().material = ColorMaterial;
                EndPoint.GetComponent<MeshRenderer>().material = ColorMaterial;

            }
        }

        private List<Vector3> OutLineCircle(Vector3 startPosition, float outLineDistance,float startAngle, float totalAngle, int stepCount)
        {
            //Todo:Done outlining
            List<Vector3> circleDots = MeshGenerator.GetDotsAround(outLineDistance,stepCount,startAngle,)
        }

        private Vector3[] OutLineCurveLeft(Curve curve,float distance)
        {
           List<Vector3> result= new List<Vector3>();
            for (int i = 0; i < curve.CurveDots.Length; i++)
            {
                Vector3 dot = MeshGenerator.AngleToPoint(distance, (curve.CurveDots[i].Angle.z+90)*Mathf.Deg2Rad);
                dot += curve.CurveDots[i].Point;
                result.Add(dot);
            }

            return result.ToArray();
        }
        private Vector3[] OutLineCurveRight(Curve curve,float distance)
        {
           List<Vector3> result= new List<Vector3>();
            for (int i = 0; i < curve.CurveDots.Length; i++)
            {
                Vector3 dot = MeshGenerator.AngleToPoint(distance, (curve.CurveDots[i].Angle.z-90)*Mathf.Deg2Rad);
                dot += curve.CurveDots[i].Point;
                result.Add(dot);
            }

            return result.ToArray();
        }
    }
}