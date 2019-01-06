using UnityEngine;

namespace DomainF
{
    public class TargetBallBehaviour : MonoBehaviour, IShowable
    {
        [SerializeField] private GameObject targetObject;

        public bool Visible
        {
            set
            {
                targetObject.SetActive(value);
            }
        }
    }
}