using UnityEngine;

namespace GameQuest
{
    class FingerQuest : MonoBehaviour
    {
        public FingerTask[] FingerTasks;

        public void Start()
        {
            FingerTasks[0].InitTask();

        }
    }
}
