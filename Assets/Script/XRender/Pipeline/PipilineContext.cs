using UnityEngine;

namespace XRender
{
    public class VertexShaderContext
    {
        public Vector4 Vertex;
        public Color Color;

        public VertexShaderContext(Vector4 vertex, Color color)
        {
            Vertex = vertex;
            Color = color;
        }
    }

    public class CanonicalCubeVertexContext
    {
        public Vector4 Vertex;
        public Color Color;
        
        public CanonicalCubeVertexContext(Vector4 vertex, Color color)
        {
            Vertex = vertex;
            Color = color;
        }
    }

    public class ScreenVertexContext
    {
        public Vector4 Vertex;
        public Color Color;

        public ScreenVertexContext(Vector4 vertex, Color color)
        {
            Vertex = vertex;
            Color = color;
        }
    }
    
    public class FragShaderContext
    {
        public int PosX;
        public int PosY;
        public Color Color;


        public FragShaderContext(int posX, int posY, Color color)
        {
            PosX = posX;
            PosY = posY;
            Color = color;
        }
    }
    
    
}