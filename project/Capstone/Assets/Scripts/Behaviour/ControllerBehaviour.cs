using System;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

namespace DomainF
{
    public interface IControllerBehaviour
    {
        float LaserLength { set; }
        string IndicatorText { set; }
        event Action TriggerPressed;
        event Action TriggerReleased;
        event Action<Transform> TransformChanged;
        event Action<Vector2> ThumbstickPositionChanged;
    }
    
    public class ControllerBehaviour : MonoBehaviour, IControllerBehaviour
    {
        [SerializeField] private GameObject targetSphere;
        [SerializeField] private Text indicatorText;
        
        private LineRenderer laserPointer_;
        private float laserLength_ = 20f;
        
        private void Start()
        {
            laserPointer_ = GetComponentInChildren<LineRenderer>();
        }
        
        private void Update()
        {
            laserPointer_.SetPosition(0, transform.position);
            var endPoint =  transform.forward * laserLength_;
            targetSphere.transform.position = endPoint;
            laserPointer_.SetPosition(1, endPoint);
        }

        public void UnityEvent_OnTransformChanged()
        {
            if (TransformChanged == null)
                return;

            TransformChanged.Invoke(transform);
        }

        public void UnityEvent_OnThumbstickPositionChanged(SteamVR_Action_Vector2 position)
        {
            if (ThumbstickPositionChanged == null)
                return;
            
            ThumbstickPositionChanged.Invoke(position.GetAxis(SteamVR_Input_Sources.Any));
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

        public float LaserLength
        {
            set { laserLength_ = value; }
        }

        public string IndicatorText
        {
            set { indicatorText.text = value;  }
        }
        
        public event Action TriggerPressed;
        public event Action TriggerReleased;
        public event Action<Transform> TransformChanged;
        public event Action<Vector2> ThumbstickPositionChanged;
    }
}