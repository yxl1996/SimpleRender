using Render.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Window
{
    public class RenderWindow : EditorWindow
    {
        private static EditorWindow _window;

        [MenuItem("CustomRender/RenderWindow &2")]
        static void CreateWindow()
        {
            GetWindow<RenderWindow>("渲染窗口");
        }

        public static bool IsOpen;
        
        public static Texture2D Texture2D
        {
            set
            {
                var window = GetWindow<RenderWindow>();
                
                window.minSize = window.maxSize = new Vector2(value.width, value.height);
                if(_image != null)
                    _image.image = value;
            }
        }

        private static Image _image;

        private bool _isInit;
        
        private void OnEnable()
        {
            _isInit = false;
            IsOpen = true;
        }

        private void OnDisable()
        {
            _isInit = true;
            IsOpen = false;
        }

        private void OnGUI()
        {
            if (!_isInit)
            {
                _isInit = true;
                
                _image = new Image();
                rootVisualElement.Add(_image);
                Texture2D = AssetDatabase.LoadAssetAtPath<Texture2D>(PathUtil.SampleTexturePath);
            }   
        }
    }
}