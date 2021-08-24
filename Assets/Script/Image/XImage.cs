using UnityEngine;

namespace Render.Image
{
    public class XImage : IXImage
    {
        public Texture2D Texture;
        
        public XImage(Texture2D texture)
        {
            Texture = texture;
        }
        
        public Color GetPixel(int x, int y)
        {
            return Texture.GetPixel(x, y);
        }

        public void SetPixel(int x, int y, Color color)
        {
            Texture.SetPixel(x, y, color);
        }

        public void Clear()
        {
            for (int i = 0,width = Texture.width; i < width; i++)
            {
                for (int j = 0,height = Texture.height; j < height; j++)
                {
                    Texture.SetPixel(i, j, Color.black);
                }
            }
        }
    }
}