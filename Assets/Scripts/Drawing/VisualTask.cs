using System.Collections.Generic;
using LevelDesignTools;
using UnityEngine;

namespace Drawing
{
    public class VisualTask : MonoBehaviour
    {
        public GameObject StartPoint;
        public GameObject EndPoint;
        public GameObject OutLine;
        public Material ColorMaterial;

        public void Start()
        {

        }

        public void DrawCurve(Curve curve)
        {
            if (curve != null && curve.CurveDots != null)
            {


                MeshFilter outlineMesh = OutLine.GetComponent<MeshFilter>();
                MeshFilter startPointMesh = StartPoint.GetComponent<MeshFilter>();
                MeshFilter endPointMesh = EndPoint.GetComponent<MeshFilter>();

                Vector3[] lineOutLine = OutLineCurveLeft(curve);
                // Mesh generation
                outlineMesh.mesh = MeshGenerator.MeshFromCurve(curve);
                startPointMesh.mesh = MeshGenerator.CircleMesh(GizmosSettings.LineFatSize/2f,180,GizmosSettings.CircleStepSize);
                endPointMesh.mesh = MeshGenerator.CircleMesh(GizmosSettings.LineFatSize/2f,180,GizmosSettings.CircleStepSize);

                StartPoint.transform.localPosition = curve.CurveDots[0].Point;
                StartPoint.transform.eulerAngles =curve.CurveDots[0].Angle + Vector3.forward*90;

                int last = curve.CurveDots.Length - 1;
                EndPoint.transform.localPosition = curve.CurveDots[last].Point;
                EndPoint.transform.eulerAngles =curve.CurveDots[last].Angle - Vector3.forward*90;


                OutLine.GetComponent<MeshRenderer>().material = ColorMaterial;
                StartPoint.GetComponent<MeshRenderer>().material = ColorMaterial;
                EndPoint.GetComponent<MeshRenderer>().material = ColorMaterial;

            }
        }

        private Vector3[] OutLineCurveLeft(Curve curve)
        {
           List<Vector3> result= new List<Vector3>();
            for (int i = 0; i < curve.CurveDots.Length; i++)
            {
            }

            return result.ToArray();
        }
    }
}