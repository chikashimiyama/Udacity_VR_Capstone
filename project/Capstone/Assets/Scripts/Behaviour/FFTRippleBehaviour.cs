using System;
using UnityEngine;


namespace DomainF
{
    public class FFTRippleBehaviour : MonoBehaviour, IVisualizerBehaviour, IVisible
    {
        private const float Radius = 3f;
        private const int FftFrameSize = 512;
        private const float Step = Mathf.PI * 2f / FftFrameSize;

        [SerializeField] private Shader fftShader_;
        private Mesh mesh_;
        private Material material_;
        private const int FftSize = 512;
        private const int numberOfRipples = 10;
        private const int totalDataSize = FftSize * numberOfRipples;

        public void Visualize(float[] data)
        {
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
                    var point = new Vector3 {x = Mathf.Sin(radian) * Radius, y = 0f, z = Mathf.Cos(radian) * Radius};
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