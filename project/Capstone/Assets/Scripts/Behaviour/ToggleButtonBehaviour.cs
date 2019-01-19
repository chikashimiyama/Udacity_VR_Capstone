using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DomainF
{
    public interface IToggleButtonBehaviour
    {
        bool State { set; }
        event Action ButtonTouched;
    }
    
    public class ToggleButtonBehaviour : MonoBehaviour, IToggleButtonBehaviour
    {
        [SerializeField] private GameObject activeObject;
        [SerializeField] private GameObject inactiveObject;

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