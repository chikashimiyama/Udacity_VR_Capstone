using System;
using UnityEngine;
using Valve.VR;

namespace DomainF
{
    public interface IControllerBehaviour
    {
        void DrawLaser();
        void DrawWaveform(float[] samples);
        
        float LaserLength { set; }
        event Action TriggerPressed;
        event Action TriggerReleased;
        event Action<Transform> TransformChanged;
        event Action<Vector2> ThumbstickPositionChanged;
        event Action Updated;
    }
    
    public class ControllerBehaviour : MonoBehaviour, IControllerBehaviour
    {
        [SerializeField] private GameObject targetSphere;
        [SerializeField] private GameObject waveform;
        
        private LineRenderer laserPointer_;
        private LineRenderer waveform_;
        private float laserLength_ = 20f;
        private IPureDataArrayFacade carrierArray_;
        private IPureDataArrayFacade modulatorArray_;
        
        private void Start()
        {
            laserPointer_ = GetComponentInChildren<LineRenderer>();
            waveform_ = waveform.GetComponent<LineRenderer>();
            carrierArray_ = new PureDataArrayFacade("carrier", 512);
            modulatorArray_ = new PureDataArrayFacade("modulator", 512);
        }
        
        private void Update()
        {
            if(Updated != null)
                Updated.Invoke();
        }

        public void DrawLaser()
        {
            laserPointer_.SetPosition(0, transform.position);
            var endPoint =  transform.forward * laserLength_ + transform.position;
            targetSphere.transform.position = endPoint;
            laserPointer_.SetPosition(1, endPoint);
        }

        public void DrawWaveform(float[] samples)
        {
            var waveStep = laserLength_ / 512f;
            var current = 0.0f;

            for(var i = 0; i < 512; i++)
            {
                var vertex = transform.forward * current + transform.position;
                vertex.x += samples[i] * 0.1f ;
                waveform_.SetPosition(i, vertex);
                current += waveStep;
            }
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
        
        public event Action TriggerPressed;
        public event Action TriggerReleased;
        public event Action<Transform> TransformChanged;
        public event Action<Vector2> ThumbstickPositionChanged;
        public event Action Updated;
    }
}