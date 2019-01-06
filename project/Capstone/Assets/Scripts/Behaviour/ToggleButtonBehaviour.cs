using System;
using UnityEngine;

namespace DomainF
{
    public interface IToggleButtonBehaviour
    {
        bool State { set; }
        bool Anotate { set; }
        event Action ButtonTouched;
    }
    
    public class ToggleButtonBehaviour : MonoBehaviour, IToggleButtonBehaviour
    {
        
        [SerializeField] private GameObject activeObject;
        [SerializeField] private GameObject inactiveObject;
        [SerializeField] private GameObject anotationObject;

        public bool State
        {
            set
            {
                if (value)
                {
                    activeObject.SetActive(true);
                    inactiveObject.SetActive(false);
                }
                else
                {
                    activeObject.SetActive(false);
                    inactiveObject.SetActive(true);
                }
            }
        }

        public bool Anotate
        {
            set
            {
                if(anotationObject != null)
                    anotationObject.SetActive(value);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("hand"))
            {
                if(ButtonTouched != null)
                    ButtonTouched.Invoke();
            }
        }
        
        public event Action ButtonTouched;
    }
}