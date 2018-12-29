using System;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

namespace DomainF
{
    public interface IControllerBehaviour
    {
        float LaserLength { set; }
        event Action TriggerPressed;
        event Action TriggerReleased;
        event Action<Transform> TransformChanged;
        event Action<Vector2> ThumbstickPositionChanged;
    }
    
    public class ControllerBehaviour : MonoBehaviour, IControllerBehaviour
    {
        [SerializeField] private GameObject targetSphere;
        [SerializeField] private GameObject waveform;
        
        private LineRenderer laserPointer_;
        private LineRenderer waveform_;
        private float laserLength_ = 20f;
        private IPureDataArrayFacade pureDataArrayFacade_;
        
        private void Start()
        {
            laserPointer_ = GetComponentInChildren<LineRenderer>();
            waveform_ = waveform.GetComponent<LineRenderer>();
            pureDataArrayFacade_ = new PureDataArrayFacade("carrier", 512);
        }
        
        private void Update()
        {
            var handPos = transform.position;
            DrawLaser(handPos);
            DrawWaveform(handPos);
        }

        private void DrawLaser(Vector3 origin)
        {
            laserPointer_.SetPosition(0, origin);
            var endPoint =  transform.forward * laserLength_ + origin;
            targetSphere.transform.position = endPoint;
            laserPointer_.SetPosition(1, endPoint);
        }

        private void DrawWaveform(Vector3 origin)
        {
            pureDataArrayFacade_.Update();
            var waveStep = laserLength_ / 512f;
            var samples = pureDataArrayFacade_.Get();
            var current = 0.0f;

            for(var i = 0; i < 512; i++)
            {
                var vertex = transform.forward * current + origin;
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
    }
}