using System;
using UnityEngine;

namespace DomainF
{
    public interface IOutputWaveformBehaviour
    {
        void DrawWaveform(float[] samples);
        float Distance { get; }
        event Action Updated;
    }
    
    public class OutputWaveformBehaviour : MonoBehaviour, IOutputWaveformBehaviour
    {
        [SerializeField] private GameObject targetA;
        [SerializeField] private GameObject targetB;
        private LineRenderer waveformRenderer_;

        private void Start()
        {
            waveformRenderer_ = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (Updated != null) Updated.Invoke();
        }
        
        public void DrawWaveform(float[] samples)
        {
            var vector = targetB.transform.position - targetA.transform.position;
            var ampVector = Vector3.Normalize(Quaternion.Euler(0, 0, 90) * vector);
            
            var waveStep = vector / 512f;
            var currentPosition = targetA.transform.position;
            for (var i = 0; i < 512; i++)
            {
                var vertex = currentPosition;
                vertex += samples[i] * ampVector;
                waveformRenderer_.SetPosition(i, vertex);
                currentPosition += waveStep;
            }
        }

        public float Distance
        {
            get { return Vector3.Distance(targetA.transform.position, targetB.transform.position); }
        }

        public event Action Updated;
    }
}