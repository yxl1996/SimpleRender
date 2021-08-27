using Define;
using Render.Image;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using Window;

namespace Render.Utility
{
    public class ImageUtil
    {
        [MenuItem("Render/test1")]
        static void Test()
        {
            IXImage img = GetSampleImg(750, 750);
            SaveXImage(img);
        }

        public static IXImage GetGameSizeSampleImg()
        {
            return GetSampleImg(RenderDefine.ScreenWidth, RenderDefine.ScreenHeight);
        }
        
        public static IXImage GetSampleImg(int width,int height)
        {
            XImage img= GetXImage(width,height,PathUtil.SampleTexturePath);
            img.Clear();
            img.Texture.Apply();
            return img;
        }

        public static XImage GetXImage(int width,int height,string relativePath)
        {
            var texture2D = AssetDatabase.LoadAssetAtPath<Texture2D>(relativePath);
            if (texture2D == null || texture2D.width != width || texture2D.height != height)
            {
                texture2D = new Texture2D(width, height);
                AssetDatabase.CreateAsset(texture2D, relativePath);
            }
            return new XImage(texture2D);
        }

        public static void SaveXImage(IXImage img)
        {
            var xImage = (XImage) img;
            Assert.IsNotNull(AssetDatabase.GetAssetPath(xImage.Texture));
            xImage.Texture.Apply();
            if(RenderWindow.IsOpen)
                RenderWindow.Texture2D = xImage.Texture;
            EditorUtility.SetDirty(xImage.Texture);
            AssetDatabase.SaveAssets();
        }
    }
}


