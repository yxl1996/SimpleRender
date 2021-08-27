using UnityEngine;

namespace XRender
{
    public class CameraUtil
    {
        public static void GetOrthogonalViewPort(Camera camera, out float left, out float right, out float top,
            out float bottom)
        {
            left = -camera.aspect * camera.orthographicSize;
            right = camera.aspect * camera.orthographicSize;
            top = camera.orthographicSize;
            bottom = -camera.orthographicSize;
            // Debug.Log($"left:{left}");
            // Debug.Log($"right:{right}");
            // Debug.Log($"top:{top}");
            // Debug.Log($"bottom:{bottom}");
        }

        public static void GetPerspectiveViewPort(Camera camera, out float left, out float right, out float top,
            out float bottom)
        {
            top = Mathf.Tan(camera.fieldOfView/2f) * camera.nearClipPlane;
            right = camera.aspect * top;
            left = -right;
            bottom = -top;
        }
    }
}