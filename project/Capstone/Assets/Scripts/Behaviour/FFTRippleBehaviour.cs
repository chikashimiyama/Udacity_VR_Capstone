using System;
using UnityEngine;

namespace DomainF
{
    public class FFTRippleBehaviour : MonoBehaviour, IVisualizerBehaviour, IVisible
    {
        private const float Offset = 30f;
        private const float Distance = 1f;

        private const int FftFrameSize = 512;
        private const float Step = Mathf.PI * 2f / FftFrameSize;

        [SerializeField] private ComputeShader rippleComputeShader;
        [SerializeField] private Shader fftShader;
        
        private Mesh mesh_;
        private Material material_;
        private const int FftSize = 512;
        private const int NumberOfRipples = 32;
        private const int TotalDataSize = FftSize * NumberOfRipples;

        private int kernelIndex_;
        private ComputeBuffer rippleComputeBuffer_;
        private ComputeBuffer updatedBuffer_;
        private MeshRenderer meshRenderer_;

        public void Visualize(float[] data)
        {   
            updatedBuffer_.SetData(data);
            rippleComputeShader.Dispatch(kernelIndex_, NumberOfRipples, 1, 1);
        }

        private void Start()
        {
            meshRenderer_ = GetComponent<MeshRenderer>();
            rippleComputeBuffer_ = new ComputeBuffer(TotalDataSize , sizeof(float));
            updatedBuffer_ = new ComputeBuffer(FftSize , sizeof(float));

            kernelIndex_ = rippleComputeShader.FindKernel("FFTRipple");
            rippleComputeShader.SetBuffer(kernelIndex_, "Data", rippleComputeBuffer_);
            rippleComputeShader.SetBuffer(kernelIndex_, "UpdatedData", updatedBuffer_);

            var meshFilter = GetComponent<MeshFilter>();

            mesh_ = new Mesh {vertices = FillVertices()};
            mesh_.SetIndices(FillIndices(), MeshTopology.Points, 0);
            mesh_.RecalculateBounds();
            meshFilter.mesh = mesh_;
            material_ = new Material(fftShader);
            material_.SetBuffer("fftData", rippleComputeBuffer_);

            var meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = material_;
        }

        private void Update()
        {
            if (Updated != null) 
                Updated.Invoke();
        }

        private void OnRenderObject()
        {
            material_.SetPass(0);
            Graphics.DrawProcedural(MeshTopology.Points, TotalDataSize);
        }

        public bool State
        {
            set { meshRenderer_.enabled = value; }
        }

        private static Vector3[] FillVertices()
        {
            var vertices = new Vector3[TotalDataSize];
            for (var i = 0; i < NumberOfRipples; i++)
            {
                for (var j = 0; j < FftFrameSize; j++)
                {
                    var radian = Step * j;
                    var x = Mathf.Sin(radian) * (Offset + Distance * i);
                    var z = Mathf.Cos(radian) * (Offset + Distance * i);
                    var point = new Vector3 {x = x , y = 0f, z = z};
                    vertices[i * FftFrameSize + j] = point;
                }
            }
            return vertices;
        }

        private static int[] FillIndices()
        {
            var indices = new int[TotalDataSize];
            for (var i = 0; i < TotalDataSize; ++i)
                indices[i] = i;
            return indices;
        }

        public event Action Updated;
    }
}