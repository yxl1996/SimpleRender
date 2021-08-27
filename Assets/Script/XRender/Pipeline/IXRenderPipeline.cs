using System;
using UnityEngine;

namespace XRender
{
    public interface IXRenderPipeline
    {
        IXRenderPipeline Set(Vector3[] vertices, int[] triangles, Color[] colors);
        IXRenderPipeline SetVertexShader(Func<VertexShaderContext, CanonicalCubeVertexContext> vertexShader);
        IXRenderPipeline SetFragmentShader(Func<FragShaderContext,Color> fragmentShader);
        void Draw();
    }
}