using Define;
using UnityEngine;

namespace XRender
{
    public class TriangleUtil
    {
        public static bool IsInTriangle(Vector2 pixel,Vector2 vertex1,Vector2 vertex2,Vector2 vertex3)
        {
            Vector2 v12 = vertex2 - vertex1;
            Vector2 v23 = vertex3 - vertex2;
            Vector2 v31 = vertex1 - vertex3;

            Vector2 v1p = pixel - vertex1;
            Vector2 v2p = pixel - vertex2;
            Vector2 v3p = pixel - vertex3;

            float z1 = v12.x * v1p.y - v12.y * v1p.x;
            float z2 = v23.x * v2p.y - v23.y * v2p.x;
            float z3 = v31.x * v3p.y - v31.y * v3p.x;

            if (z1 > 0 && z2 > 0 && z3 > 0)
                return true;
            if (z1 < 0 && z2 < 0 && z3 < 0)
                return true;

            return false;
        }

        public static void GetTriangleMinBoundingBox(Vector2 v1,Vector2 v2,Vector2 v3,out int minWidth,out int maxWidth,
            out int minHeight,out int maxHeight)
        {
            minWidth = (int) Mathf.Min(Mathf.Min(v1.x, v2.x), v3.x);
            maxWidth = (int) Mathf.Max(Mathf.Max(v1.x, v2.x), v3.x);
            minHeight = (int) Mathf.Min(Mathf.Min(v1.y, v2.y), v3.y);
            maxHeight = (int) Mathf.Max(Mathf.Max(v1.y, v2.y), v3.y);

            minWidth = Mathf.Max(0, minWidth);
            maxWidth = Mathf.Min(RenderDefine.ScreenWidth, maxWidth);
            minHeight = Mathf.Max(0, minHeight);
            maxHeight = Mathf.Min(RenderDefine.ScreenHeight, maxHeight);
        }
        
    }
}