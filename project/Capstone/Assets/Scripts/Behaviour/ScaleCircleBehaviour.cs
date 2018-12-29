using UnityEngine;

namespace DomainF
{
    public class ScaleCircleBehaviour : MonoBehaviour
    {
        private LineRenderer circle_;
        private const int NumVertices = 60;
        private const float Step = 360f / NumVertices;

        private void Start()
        {
            circle_ = gameObject.GetComponent<LineRenderer>();
            circle_.loop = true;
            CreatePoints();
        }

        public bool Visibility
        {
            set { enabled = value; }
        }

        private void CreatePoints()
        {
            var current = 0f;
            for (var i = 0; i < NumVertices; i++)
            {
                var x = Mathf.Sin(Mathf.Deg2Rad * current);
                var z = Mathf.Cos(Mathf.Deg2Rad * current);

                circle_.SetPosition(i, new Vector3(x, 0, z));
                current += Step;
            }
        }
    }
}