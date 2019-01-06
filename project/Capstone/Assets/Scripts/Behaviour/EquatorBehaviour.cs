using UnityEngine;

namespace DomainF
{
    public  interface IColor
    {
        Color color { set; }
    }
    
    public class EquatorBehaviour : MonoBehaviour, IShowable, IColor
    {
        [SerializeField] private GameObject equator;

        private void Start()
        {
        }
        
        
        public bool Visible
        {
            set { equator.SetActive(value); }
        }

        public Color color
        {
            set
            {
                
            }
        }
    }
}