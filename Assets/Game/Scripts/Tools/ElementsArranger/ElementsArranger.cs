using UnityEngine;

namespace Tools
{
    [ExecuteAlways]
    public class ElementsArranger : MonoBehaviour
    {
        [SerializeField] private float spacing = 2f;
        [SerializeField] private Vector3 _rotation;

        void UpdateLayout()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Vector3 pos = new Vector3(i * spacing, 0, 0);
                transform.GetChild(i).localPosition = pos;
                transform.GetChild(i).localRotation = Quaternion.Euler(_rotation);
            }
        }

        private void OnValidate() => UpdateLayout();
    }
}
