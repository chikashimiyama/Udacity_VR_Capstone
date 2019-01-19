using UnityEngine;
using UnityEngine.UI;

namespace Behaviour
{
    public interface IDirectionCircleBehaviour
    {
        void SetRotate(float angle);
        void SetLabels(string leftText, string rightText);
    }
    
    public class DirectionCircleBehaviour : MonoBehaviour, IDirectionCircleBehaviour
    {
        [SerializeField] private GameObject circleObject;
        [SerializeField] private GameObject leftLabelObject;
        [SerializeField] private GameObject leftTextField;
        [SerializeField] private GameObject rightLabelObject;
        [SerializeField] private GameObject rightTextField;

        private LineRenderer lineRenderer_;
        private const int NumVertices = 60;
        private const float Step = 360f / NumVertices;
        private Text leftText_;
        private Text rightText_;
        private const float MaxDist = 35f;

        private void Awake()
        {
            lineRenderer_ = circleObject.GetComponent<LineRenderer>();
            lineRenderer_.loop = true;
            leftText_ = leftTextField.GetComponent<Text>();
            rightText_ = rightTextField.GetComponent<Text>();

            CreatePoints();
        }
        
        private void CreatePoints()
        {
            var current = 0f;
            for (var i = 0; i < NumVertices; i++)
            {
                var x = Mathf.Sin(Mathf.Deg2Rad * current);
                var y = Mathf.Cos(Mathf.Deg2Rad * current);

                lineRenderer_.SetPosition(i, new Vector3(x, y, 0));
                current += Step;
            }
            circleObject.transform.localScale = new Vector3(MaxDist, MaxDist, 1f);
            leftLabelObject.transform.position = new Vector3(-MaxDist, 1f, 0f);
            rightLabelObject.transform.position = new Vector3(MaxDist, 1f, 0f);
        }

        public void SetRotate(float angle)
        {
            transform.Rotate(new Vector3(0, angle, 0f));
        }

        public void SetLabels(string leftText, string rightText)
        {
            leftText_.text = leftText;
            rightText_.text = rightText;
        }
    }
}