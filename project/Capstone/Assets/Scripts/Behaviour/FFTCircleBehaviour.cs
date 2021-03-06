﻿using System;
using UnityEngine;

namespace DomainF
{
    public class FFTCircleBehaviour : MonoBehaviour, IVisualizerBehaviour, IVisible
    {
        [SerializeField] private  float Scale = 150f;

        private MeshFilter meshFilter_;
        private MeshRenderer meshRenderer_;
        private Vector3[] vertices_;

        private  const float Radius = 45f;
        private const int FftFrameSize = 512;
        private const int NumLineVertices = FftFrameSize * 2;
        private const int NumTotalLineVertices = NumLineVertices * 2;
        private const int NumIndices = FftFrameSize * 6;
        private const int Offset = FftFrameSize * 2;
        private const float Step = Mathf.PI * 2f / FftFrameSize;
        private const float Gap = 0.003f;

        public void Visualize(float[] data)
        {
            var vertices = meshFilter_.mesh.vertices;
            for (var i = 0; i < FftFrameSize; ++i)
            {
                var index = i * 2;
                var height = data[i] * Scale;
                vertices[index].y = height;
                vertices[index + 1].y = height;
                vertices[index + NumLineVertices].y = -height;
                vertices[index + 1 + NumLineVertices].y = -height;
            }

            meshFilter_.mesh.vertices = vertices;
        }

        private void Start()
        {
            meshFilter_ = GetComponent<MeshFilter>();
            meshRenderer_ = GetComponent<MeshRenderer>();
            meshFilter_.mesh = new Mesh {vertices = FillVertices(), triangles = FillTriangles()};
        }

        private void Update()
        {
            if (Updated != null) Updated.Invoke();
        }

        private static Vector3[] FillVertices()
        {
            var vertices = new Vector3[NumTotalLineVertices];
            for (var i = 0; i < FftFrameSize; ++i)
            {
                SetArc(vertices, i, 0f);
                SetArc(vertices, i, 0f, NumLineVertices);
            }

            return vertices;
        }

        private static void SetArc(Vector3[] vertices, int index, float height, int offset = 0)
        {
            var radian = Step * index - Mathf.PI * 0.5f;
            var left = new Vector3 {x = Mathf.Sin(radian) * Radius, y = height, z = Mathf.Cos(radian) * Radius};
            radian += Step;
            var right = new Vector3
                {x = Mathf.Sin(radian - Gap) * Radius, y = height, z = Mathf.Cos(radian - Gap) * Radius};

            vertices[index * 2 + offset] = left;
            vertices[index * 2 + 1 + offset] = right;
        }

        private static int[] FillTriangles()
        {
            var triangles = new int[NumIndices];
            var index = 0;

            // one rectangle
            for (var i = 0; i < FftFrameSize; ++i)
            {
                var onset = i * 2;
                triangles[index++] = 0 + onset;
                triangles[index++] = 1 + onset;
                triangles[index++] = Offset + onset;

                triangles[index++] = Offset + onset;
                triangles[index++] = 1 + onset;
                triangles[index++] = Offset + 1 + onset;
            }

            return triangles;
        }

        public event Action Updated;

        public bool State
        {
            set { meshRenderer_.enabled = value; }
        }
    }
}