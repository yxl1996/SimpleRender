using Render.Image;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Render.Utility
{
    public class ImageUtil
    {
        private const int X = 13340;
        private const int Y = 750;
        
        [MenuItem("Test/test1")]
        static void Test()
        {
            
        }

        public static IXImage GetSampleImg()
        {
            XImage img= GetXImage(PathUtil.SampleTexturePath);
            img.Clear();
            img.Texture.Apply();
            return img;
        }
        
        public static void CopySprite2Texture(string spriteRelativePath,string textureRelativePath)
        {
            string spritePath = PathUtil.GetSpritePath(spriteRelativePath);
            string texturePath = PathUtil.GetTexturePath(textureRelativePath);
            
            var sT = AssetDatabase.LoadAssetAtPath<Texture2D>(spritePath);
            Assert.IsNotNull(sT);
            Assert.IsNull(AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath));
            
            var new2D = new Texture2D(sT.width, sT.height);
            IXImage xImage = new XImage(new2D);
            for (int i = 0; i < sT.width; i++)
            {
                for (int j = 0; j < sT.height; j++)
                {
                    xImage.SetPixel(i, j, sT.GetPixel(i, j));
                }
            }
            SaveXImage(xImage, texturePath);
        }
        
        public static XImage GetXImage(string relativePath,bool autoCreate = false)
        {
            var texture2D = AssetDatabase.LoadAssetAtPath<Texture2D>(relativePath);
            if (texture2D == null)
            {
                if (autoCreate)
                {
                    texture2D = new Texture2D(X,Y);
                    return new XImage(texture2D);
                }
                return null;
            }
            return new XImage(texture2D);
        }

        public static void SaveXImage(IXImage img)
        {
            var xImage = (XImage) img;
            if(string.IsNullOrEmpty(AssetDatabase.GetAssetPath(xImage.Texture)))
                return;
            xImage.Texture.Apply();
            EditorUtility.SetDirty(xImage.Texture);
            AssetDatabase.SaveAssets();
        }
        
        public static void SaveXImage(IXImage img,string relativePath)
        {
            if(string.IsNullOrEmpty(relativePath))
                return;
            
            var xImage = (XImage) img;
            AssetDatabase.CreateAsset(xImage.Texture, relativePath);
            SaveXImage(img);
        }
        
    }
}


