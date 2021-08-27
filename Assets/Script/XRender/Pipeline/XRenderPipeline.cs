using System;
using System.Collections.Generic;
using Define;
using Render.Image;
using Render.Utility;
using UnityEngine;

namespace XRender
{
    public class XRenderPipeline : IXRenderPipeline
    {
        private Vector4[] _vertices;
        private int[] _triangles;
        private Color[] _colors;

        private Func<VertexShaderContext, CanonicalCubeVertexContext> _vertexShader;
        private Func<FragShaderContext, Color> _fragmentShader;

        public IXRenderPipeline Set(Vector3[] vertices, int[] triangles, Color[] colors)
        {
            _vertices = new Vector4[vertices.Length];
            for (int i = 0,count = vertices.Length; i < count; i++)
            {
                Vector3 v = vertices[i];
                _vertices[i] = new Vector4(v.x, v.y, v.z, 1);
            }
            
            _triangles = triangles;
            _colors = colors;

            return this;
        }

        public IXRenderPipeline SetVertexShader(Func<VertexShaderContext, CanonicalCubeVertexContext> vertexShader)
        {
            _vertexShader = vertexShader;
            return this;
        }

        public IXRenderPipeline SetFragmentShader(Func<FragShaderContext, Color> fragmentShader)
        {
            _fragmentShader = fragmentShader;
            return this;
        }

        public void Draw()
        {
            //顶点着色器
            Debug.Log("=====顶点着色器=====");
            int vertexCount = _vertices.Length;
            var cubeVertices = new CanonicalCubeVertexContext[vertexCount];
            for (int i = 0 ; i < vertexCount; i++)
            {
                CanonicalCubeVertexContext  context  = _vertexShader(new VertexShaderContext(_vertices[i], _colors[i]));
                cubeVertices[i] = context;
                Debug.Log($"顶点标准立方体坐标:{context.Vertex}");
            }

            //裁剪标准立方体外的顶点
            //TODO 
            Debug.Log("=====剪裁=====");
            Debug.Log("TODO");
            
            //剪裁空间->屏幕空间
            Matrix4x4 cube2ScreenMatrix =
                MatrixTransHelper.CanonicalCube2ScreenMatrix(RenderDefine.ScreenWidth, RenderDefine.ScreenHeight);
            var screenVertices = new ScreenVertexContext[vertexCount];
            Debug.Log("=====剪裁空间->屏幕空间=====");
            for (int i = 0 ; i < vertexCount; i++)
            {
                CanonicalCubeVertexContext cvc = cubeVertices[i];
                screenVertices[i] = new ScreenVertexContext(cube2ScreenMatrix * cvc.Vertex,cvc.Color);
                Debug.Log($"屏幕坐标:{screenVertices[i].Vertex}");
            }
            
            //三角形设置
            Debug.Log("=====三角形设置=====");
            Debug.Log("TODO");
            
            //三角形遍历
            Debug.Log("=====三角形遍历=====");
            var fragShaderContexts = new List<FragShaderContext>();
            for (int i = 0 ,count = _triangles.Length; i < count; i+=3)
            {
                ScreenVertexContext svc1 = screenVertices[_triangles[i]];
                ScreenVertexContext svc2 = screenVertices[_triangles[i+1]];
                ScreenVertexContext svc3 = screenVertices[_triangles[i+2]];

                TriangleUtil.GetTriangleMinBoundingBox(svc1.Vertex, svc2.Vertex, svc3.Vertex,
                    out int minWidth, out int maxWidth, out int minHeight, out int maxHeight);
                
                for (int width = minWidth; width < maxWidth; width++)
                {
                    for (int height = minHeight; height < maxHeight; height++)
                    {
                        var pixel = new Vector2(width + .5f, height + .5f);
                        if (TriangleUtil.IsInTriangle(pixel, svc1.Vertex,svc2.Vertex,svc3.Vertex))
                        {
                            //TODO 片元数据插值
                            var fsc = new FragShaderContext(width, height, svc1.Color);
                            fragShaderContexts.Add(fsc);
                        }
                    }
                }
            }
            
            //片元着色器
            Debug.Log("=====片元着色器=====");
            Debug.Log($"网格总计覆盖到{fragShaderContexts.Count}个像素");
            int screenWidth = RenderDefine.ScreenWidth;
            int screenHeight = RenderDefine.ScreenHeight;
            var colors = new Color[screenWidth, screenHeight];
            for (int i = 0,count = fragShaderContexts.Count; i < count; i++)
            {
                FragShaderContext fsc = fragShaderContexts[i];
                Color color = _fragmentShader(fsc);
                colors[fsc.PosX, fsc.PosY] = color;
                //Debug.Log($"像素位置:({fsc.PosX},{fsc.PosY}) 颜色:{color}");
            }

            //生成图片
            Color background = RenderDefine.BackGroundColor;
            IXImage xImage = ImageUtil.GetGameSizeSampleImg();
            for (int width = 0; width < RenderDefine.ScreenWidth; width++)
            {
                for (int height = 0; height < RenderDefine.ScreenHeight; height++)
                {
                    xImage.SetPixel(width, height,
                        colors[width, height] != default ? colors[width, height] : background);
                }
            }
            ImageUtil.SaveXImage(xImage);
        }
    }
}