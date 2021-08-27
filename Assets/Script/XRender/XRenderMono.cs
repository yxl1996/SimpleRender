using UnityEngine;

namespace XRender
{
    public class XRenderMono  : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private XMesh xMesh;

        private Mesh Mesh => xMesh.Mesh;

        private void Start()
        {
            IXRenderPipeline pipeline = new XRenderPipeline();
            
            pipeline
                .Set(Mesh.vertices, Mesh.triangles, Mesh.colors)
                .SetVertexShader(vertexIn =>
                {
                    // Debug.Log("=======================");
                    // Debug.Log(vertexIn.Vertex);
                    Vector4 vertex = MatrixTransHelper.MvpTrans(camera,BuildModelData(xMesh.transform), vertexIn.Vertex);
                    return new CanonicalCubeVertexContext(vertex, vertexIn.Color);
                })
                .SetFragmentShader(fragIn =>
                {
                    return fragIn.Color;
                })
                .Draw();
        }

        private ModelData BuildModelData(Transform t)
        {
            return new ModelData
            {
                Position = t.position,
                Rotation = t.rotation,
                Scale = t.localScale
            };
        }
    }
}