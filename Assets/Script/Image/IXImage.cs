using UnityEngine;

namespace Render.Image
{
    public interface IXImage
    {
        Color GetPixel(int x, int y);
        void SetPixel(int x, int y, Color color);
    }
}