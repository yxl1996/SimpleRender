
namespace Render.Utility
{
    public class PathUtil
    {
        public static readonly string SpritePath;
        public static readonly string TexturePath;
        public static readonly string SampleTexturePath;
        
        static PathUtil()
        {
            SpritePath = "Assets/Resources/Sprite";
            TexturePath = "Assets/Resources/Texture";
            SampleTexturePath = $"{TexturePath}/sample.rendertexture";
        }
        
        public static string GetSpritePath(string relativePath)
        {
            return $"{SpritePath}/{relativePath}";
        }
        
        public static string GetTexturePath(string relativePath)
        {
            return $"{TexturePath}/{relativePath}";
        }
        
    }
}