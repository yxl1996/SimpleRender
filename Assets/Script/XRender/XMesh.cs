using UnityEngine;

namespace XRender
{
    [RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
    public class XMesh : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        public Mesh Mesh => _meshFilter.mesh;
        
        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            
            var mesh  = new Mesh();
            var vertices = new Vector3[4];
            vertices[0] = new Vector3(-1,-1,0);
            vertices[1] = new Vector3(-1,1,0);
            vertices[2] = new Vector3(1,1,0);
            vertices[3] = new Vector3(1,-1,0);

            var triangles = new [] {0, 1, 2, 2, 3, 0};

            var colors = new Color[4];
            Color col = Color.cyan;
            colors[0] = col;
            colors[1] = col;
            colors[2] = col;
            colors[3] = col;
            
            var uv = new Vector2[4];
            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 1);
            uv[2] = new Vector2(1, 1);
            uv[3] = new Vector2(1, 0);
            
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.colors = colors;
            mesh.name = "XMesh";
            mesh.uv = uv;
            mesh.RecalculateNormals();
            _meshFilter.mesh = mesh;
        }
    }
}