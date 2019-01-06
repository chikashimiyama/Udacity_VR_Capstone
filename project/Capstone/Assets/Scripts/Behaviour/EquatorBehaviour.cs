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

        private void Start()
        {
        }
        
        
        public bool State
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