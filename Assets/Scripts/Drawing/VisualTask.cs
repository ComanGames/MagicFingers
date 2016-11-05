using System.Collections.Generic;
using System.Linq;
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
        public LineSequence Sequence;
        public Material MainMaterial;
        public Material OutlineMaterial;

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
                MeshFilter lineMeshFilter = Line.GetComponent<MeshFilter>();

                // Mesh generation
                Mesh lineMesh = MeshGenerator.MeshFromCurve(curve, GizmosSettings.LineFatSize);
//                lineMeshFilter.mesh = lineMesh;
                startPointMesh.mesh = MeshGenerator.CircleMesh(GizmosSettings.LineFatSize/2f,180,GizmosSettings.CircleStepSize);
                endPointMesh.mesh = MeshGenerator.CircleMesh(GizmosSettings.LineFatSize/2f,180,GizmosSettings.CircleStepSize);

                Vector3 startPointAngle = (curve.CurveDots[0].Angle + Vector3.forward*90);
                Vector3 endPointAngle = curve.CurveDots[last].Angle - Vector3.forward*90;

                //Circle locations
                StartPoint.transform.localPosition = curve.CurveDots[0].Point;
                StartPoint.transform.eulerAngles =startPointAngle;

                EndPoint.transform.localPosition = curve.CurveDots[last].Point;
                EndPoint.transform.eulerAngles =endPointAngle;

                //drawing outline
                float outLineDistance = (GizmosSettings.LineFatSize +GizmosSettings.OutLineFatSize)/2.0f;

                List<Vector3> outlineDots = new List<Vector3>();
                outlineDots.AddRange(OutLineCircle(StartPoint.transform.position,outLineDistance,curve.CurveDots[0].Angle.z+90, 180, GizmosSettings.CircleStepSize));
                outlineDots.RemoveAt(outlineDots.Count-1);
                outlineDots.AddRange(OutLineCurveByAngle(curve,outLineDistance,-90));
                List<Vector3> outLineCircle = OutLineCircle(EndPoint.transform.position, outLineDistance, curve.CurveDots[last].Angle.z + -90, 180, GizmosSettings.CircleStepSize);
                outLineCircle.RemoveAt(0);
                outLineCircle.RemoveAt(outLineCircle.Count-1);
                outlineDots.AddRange(outLineCircle.ToArray());

                outlineDots.AddRange(OutLineCurveByAngle(curve,outLineDistance,90).Reverse());

                //Todo:fix first and last part of mesh
                OutLine.GetComponent<MeshFilter>().mesh = MeshGenerator.MeshFromCurve(new Curve(outlineDots.ToArray()), GizmosSettings.OutLineFatSize);

                if(lineMesh!=null)
                    Sequence.SetMeshes(MeshGenerator.MeshLineToSquares(lineMesh),MainMaterial);

                //Setting Materials
                OutLine.GetComponent<MeshRenderer>().material = MainMaterial;
                StartPoint.GetComponent<MeshRenderer>().material =MainMaterial;
                EndPoint.GetComponent<MeshRenderer>().material = MainMaterial;
                OutLine.GetComponent<MeshRenderer>().material = OutlineMaterial;

            }
        }

        public void SetActiveAt(int count,CurveDot dot)
        {
           Sequence.SetActiveCount(count);
            EndPoint.transform.localPosition = dot.Point;
            EndPoint.transform.localEulerAngles = dot.Angle + (Vector3.back*90);
        }
        private List<Vector3> OutLineCircle(Vector3 startPosition, float outLineDistance,float startAngle, float totalAngle, int stepCount)
        {
            float stepAngle = (totalAngle/stepCount)*Mathf.Deg2Rad;
            //Todo:Done outlining
            List<Vector3> circleDots = MeshGenerator.GetDotsAround(outLineDistance, stepCount, startAngle*Mathf.Deg2Rad, stepAngle);

            //Removing two first and last one
            for (int i = 0; i < circleDots.Count; i++)
            {
                circleDots[i] += startPosition;
            }
            return circleDots;
        }

        private Vector3[] OutLineCurveByAngle(Curve curve,float distance,float angle)
        {
           List<Vector3> result= new List<Vector3>();
            for (int i = 0; i < curve.CurveDots.Length; i++)
            {
                Vector3 dot = MeshGenerator.AngleToPoint(distance, (curve.CurveDots[i].Angle.z+angle)*Mathf.Deg2Rad);
                dot += curve.CurveDots[i].Point;
                result.Add(dot);
            }

            return result.ToArray();
        }
    }
}