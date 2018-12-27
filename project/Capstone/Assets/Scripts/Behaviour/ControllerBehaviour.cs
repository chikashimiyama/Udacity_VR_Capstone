using System;
using UnityEngine;

namespace DomainF
{
    public interface IControllerBehaviour
    {
        event Action TriggerPressed;
        event Action TriggerReleased;
    }
    
    public class ControllerBehaviour : MonoBehaviour, IControllerBehaviour
    {
        [SerializeField] private GameObject targetSphere;

        private LineRenderer laserPointer_;

        private void Start()
        {
            laserPointer_ = GetComponentInChildren<LineRenderer>();
        }
        
        private void Update()
        {
            laserPointer_.SetPosition(0, transform.position);
            var endPoint =  transform.forward * 30;
            targetSphere.transform.position = endPoint;
            laserPointer_.SetPosition(1, endPoint);
        }
        
        public void UnityEvent_OnTriggerPressed()
        {
            if (TriggerPressed != null) 
                TriggerPressed.Invoke();
        }

        public void UnityEvent_OnTriggerReleased()
        {
            if (TriggerReleased != null) 
                TriggerReleased.Invoke();
        }

        public event Action TriggerPressed;
        public event Action TriggerReleased;
    }
}