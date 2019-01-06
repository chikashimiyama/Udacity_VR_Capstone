using UnityEngine;

namespace DomainF
{
    public  interface IColor
    {
        Color color { set; }
    }
    
    public class EquatorBehaviour : MonoBehaviour, IVisible, IColor
    {
        [SerializeField] private GameObject equator;
        private Material material_;

        private void Start()
        {
            material_ = equator.GetComponent<Renderer>().material;
        }
               
        public bool State
        {
            set { equator.SetActive(value); }
        }

        public Color color
        {
            set
            {
                var col = value;
                material_.color = col;
            }
        }
    }
}