using System;
using UnityEngine;


namespace DomainF
{
    public class FFTRippleBehaviour : MonoBehaviour, IVisualizerBehaviour, IVisible
    {
        private const float Offset = 15f;
        private const float Distance = 3f;

        private const int FftFrameSize = 512;
        private const float Step = Mathf.PI * 2f / FftFrameSize;

        [SerializeField] private Shader fftShader_;
        private Mesh mesh_;
        private Material material_;
        private const int FftSize = 512;
        private const int numberOfRipples = 10;
        private const int totalDataSize = FftSize * numberOfRipples;

        private int count_ = 0;
        
        public void Visualize(float[] data)
        {
            material_.SetFloatArray("_Segments", data); // copy to GPU
            material_.SetInt("_Target", count_++);
            
            if (count_ == numberOfRipples)
                count_ = 0;
        }

        private void Start()
        {
            var meshFilter = GetComponent<MeshFilter>();

            mesh_ = new Mesh {vertices = FillVertices()};
            mesh_.SetIndices(FillIndices(), MeshTopology.Points, 0);
            mesh_.RecalculateBounds();
            meshFilter.mesh = mesh_;

            material_ = new Material(fftShader_);
            
            var meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = material_;
        }

        private void Update()
        {
            if (Updated != null) Updated.Invoke();
        }

        private void OnRenderObject()
        {
            material_.SetPass(0);
            Graphics.DrawProcedural(MeshTopology.Points, totalDataSize);
        }


        public bool State
        {
            set { }
        }

        private static Vector3[] FillVertices()
        {
            var vertices = new Vector3[totalDataSize];
            for (var i = 0; i < numberOfRipples; i++)
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
            var indices = new int[totalDataSize];
            for (var i = 0; i < totalDataSize; ++i)
                indices[i] = i;
            return indices;
        }

        public event Action Updated;
    }
}