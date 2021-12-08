using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.Converters
{
    class MeshConverter
    {
        public static MeshSer Serialize(MeshFilter mesh)
        {
            MeshSer ms = new MeshSer();
            Mesh m = mesh.sharedMesh;

            ms.indexFormat = m.indexFormat;
            ms.bindposes = m.bindposes;
            ms.subMeshCount = m.vertexCount;
            ms.bounds = m.bounds;
            ms.vertices = m.vertices;
            ms.normals = m.normals;
            ms.tangents = m.tangents;
            ms.uv = m.uv;
            ms.uv2 = m.uv2;
            ms.uv3 = m.uv3;
            ms.uv4 = m.uv4;
            ms.uv5 = m.uv5;
            ms.uv6 = m.uv6;
            ms.uv7 = m.uv7;
            ms.uv8 = m.uv8;
            ms.colors = m.colors;
            ms.colors32 = m.colors32;
            ms.triangles = m.triangles;
            ms.boneWeights = m.boneWeights;

            return ms;
        }
    }
}
