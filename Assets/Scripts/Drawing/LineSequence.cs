using System.Collections.Generic;
using UnityEngine;

namespace Drawing
{
    public class LineSequence : MonoBehaviour
    {
        public int Count=100;
        private List<GameObject> _parts;
        public void Awake()
        {
            _parts = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
               _parts.Add(transform.GetChild(i).gameObject); 
        }

        private void SetPartsCount(int count)
        {
            _parts=new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                _parts.Add(transform.GetChild(i).gameObject);
            }

            if (_parts.Count > count)
            {
                int countToRemove = _parts.Count - count;
                for (int i = 0; i <= countToRemove; i++)
                {
                   DestroyImmediate(_parts[_parts.Count-1]);
                    _parts.RemoveAt(_parts.Count - 1);
                }
            }

            int countToAdd = count - _parts.Count;
            for (int i = 0; i < countToAdd; i++)
            {
                GameObject toAdd = new GameObject("part - " + _parts.Count);
                toAdd.transform.parent = transform;
                toAdd.transform.localPosition= Vector3.zero;
                toAdd.AddComponent<MeshFilter>();
                toAdd.AddComponent<MeshRenderer>();
                _parts.Add(toAdd);
            }





        }

        public void SetActiveCount(int count)
        {
            for (int i = 0; i < _parts.Count; i++)
            {
                if(i<count)
                   _parts[i].SetActive(true);
                else
                    _parts[i].SetActive(false);
            } 
        }

        public void SetMeshes(Mesh[] meshes,Material material)
        {
            SetPartsCount(meshes.Length);
            for (int i = 0; i < meshes.Length; i++)
            {
                _parts[i].GetComponent<MeshFilter>().mesh = meshes[i];
                _parts[i].GetComponent<MeshRenderer>().material= material;
            }
        }
    }
}