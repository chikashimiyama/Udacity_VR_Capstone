using UnityEngine;
using UnityEngine.UI;

namespace DomainF
{
    public interface IScaleCircleBehaviour
    {
        void SetHeight(float height);
        void SetScale(float scale);
        void SetColor(Color color);
        void SetLabel(string name);
    }
    
    public class ScaleCircleBehaviour : MonoBehaviour, IScaleCircleBehaviour
    {
        [SerializeField] private GameObject circleObject;
        [SerializeField] private GameObject labelObject;
        [SerializeField] private GameObject textField;
        
        private LineRenderer lineRenderer_;
        private const int NumVertices = 60;
        private const float Step = 360f / NumVertices;
        private Text text_;

        private void Awake()
        {
            lineRenderer_ = circleObject.GetComponent<LineRenderer>();
            lineRenderer_.loop = true;
            text_ = textField.GetComponent<Text>();
            
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

                lineRenderer_.SetPosition(i, new Vector3(x, 0, z));
                current += Step;
            }
        }
        
        public void SetHeight(float height)
        {
            circleObject.transform.position = new Vector3(0, height, 0);
            var labelPos = labelObject.transform.position;
            labelPos.y = height;
            labelObject.transform.position = labelPos;

        }

        public void SetScale(float scale)
        {
            circleObject.transform.localScale = new Vector3(scale, 1f, scale);
            labelObject.transform.position = new Vector3(0f, 0f, scale);
        }

        public void SetColor(Color color)
        {
            lineRenderer_.material.color = color;
        }

        public void SetLabel(string name)
        {
            text_.text = name;
        }
    }
}