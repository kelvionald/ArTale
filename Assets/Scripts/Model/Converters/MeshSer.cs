using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.Model.Converters
{
    [Serializable]
    public class MeshSer
    {
        public Vector2[] uv1 { get; set; }
        public IndexFormat indexFormat { get; set; }
        public Matrix4x4[] bindposes { get; set; }
        public int subMeshCount { get; set; }
        public Bounds bounds { get; set; }
        public Vector3[] vertices { get; set; }
        public Vector3[] normals { get; set; }
        public Vector4[] tangents { get; set; }
        public Vector2[] uv { get; set; }
        public Vector2[] uv2 { get; set; }
        public Vector2[] uv3 { get; set; }
        public Vector2[] uv4 { get; set; }
        public Vector2[] uv5 { get; set; }
        public Vector2[] uv6 { get; set; }
        public Vector2[] uv7 { get; set; }
        public Vector2[] uv8 { get; set; }
        public Color[] colors { get; set; }
        public Color32[] colors32 { get; set; }
        public int[] triangles { get; set; }
        public BoneWeight[] boneWeights { get; set; }
    }
}
