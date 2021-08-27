using UnityEngine;

namespace XRender
{
    public static class MatrixDefine
    {
        /// <summary>
        ///平移变换矩阵
        ///1    0    0    x
        ///0    1    0    y
        ///0    0    1    z
        ///0    0    0    1
        /// </summary>
        private static readonly Matrix4x4 MoveMatrix = new Matrix4x4(
            new Vector4(1, 0, 0, -1),
            new Vector4(0, 1, 0, -1),
            new Vector4(0, 0, 1, -1),
            new Vector4(0, 0, 0, 1));
        
        /// <summary>
        ///x轴旋转矩阵 
        ///1    0    0    0
        ///0  cosX -sinX  0
        ///0  sinX  cosX  0
        ///0    0    0    1
        /// </summary>
        private static readonly Matrix4x4 XRotateMatrix = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, -1, -1, 0),
            new Vector4(0, -1,-1, 0),
            new Vector4(0, 0, 0, 1));
        
        /// <summary>
        ///y轴旋转矩阵
        ///cosY  0    sinY  0
        ///0     1     0    0
        ///-sinY 0    cosY  0
        ///0     0     0    1
        /// </summary>
        private static readonly Matrix4x4 YRotateMatrix = new Matrix4x4(
            new Vector4(-1, 0, -1, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(-1, 0,-1, 0),
            new Vector4(0, 0, 0, 1));
        
        /// <summary>
        ///z轴旋转矩阵  
        ///cosZ  -sinZ  0    0
        ///sinZ  cosZ   0    0
        ///0      0     1    0
        ///0      0     0    1
        /// </summary>
        private static readonly Matrix4x4 ZRotateMatrix = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, -1, -1, 0),
            new Vector4(0, -1,-1, 0),
            new Vector4(0, 0, 0, 1));
        
        /// <summary>
        ///缩放矩阵  
        ///x    0    0    0
        ///0    y    0    0
        ///0    0    z    0
        ///0    0    0    1
        /// </summary>
        private static readonly Matrix4x4 ScaleMatrix = new Matrix4x4(
            new Vector4(-1, 0, 0, 0),
            new Vector4(0, -1, 0, 0),
            new Vector4(0, 0, -1, 0),
            new Vector4(0, 0, 0, 1));
    }
    
    public class MatrixTransHelper
    {
        public static Vector4 MvpTrans(Camera camera,ModelData modelData,Vector4 vertex)
        {
            CameraUtil.GetOrthogonalViewPort(camera, out float left, out float right, out float top, out float bottom);
        
            Matrix4x4 model = Model2WorldMatrix(modelData.Position, modelData.Rotation, modelData.Scale);
            Matrix4x4 view = GetViewMatrix(camera.transform.position, camera.transform.rotation);
            Matrix4x4 ortho = GetOrthogonalProjectionMatrix(left, right, top, 
                bottom, camera.nearClipPlane, camera.farClipPlane);

            // Debug.Log($"after model:{model * vertex}");
            // Debug.Log($"after view:{view * model * vertex}");
            return ortho * view * model * vertex;
        }
        
        public static Matrix4x4 Model2WorldMatrix(Vector3 pos,Quaternion rotate,Vector3 scale)
        {
            return Matrix4x4.TRS(pos,rotate,scale);
        }

        public static Matrix4x4 GetViewMatrix(Vector3 pos,Quaternion rotate)
        {
            Matrix4x4 t = Matrix4x4.Translate(-pos);
            Matrix4x4 I = Matrix4x4.identity;
            I.SetRow(2, new Vector4(0, 0, -1, 0));
            t *= I;
            Matrix4x4 r = Matrix4x4.Rotate(rotate);
            return r * t;
        }

        /// <summary>
        /// 正交投影
        /// </summary>
        public static Matrix4x4 GetOrthogonalProjectionMatrix(
            float left,
            float right,
            float top,
            float bottom,
            float zNear,
            float zFar)
        {
            var ret = new Matrix4x4();
            ret.SetColumn(0, new Vector4(2f / (right - left), 0, 0, 0));
            ret.SetColumn(1, new Vector4(0, 2f / (top - bottom), 0, 0));
            ret.SetColumn(2, new Vector4(0, 0, 2f / (zNear - zFar), 0));
            ret.SetColumn(3,
                new Vector4(-(right + left) / (right - left), -(top + bottom) / (top - bottom),
                    -(zFar + zNear) / (zFar - zNear), 1));
            
            return ret;
        }
        
        /// <summary>
        /// 透视投影
        /// </summary>
        public static Matrix4x4 GetPerspectiveProjectionMatrix(float fov, float aspect, float zNear, float zFar)
        {
            return Matrix4x4.Perspective(fov, aspect, zNear, zFar);
        }

        /// <summary>
        /// 标准立方体到屏幕空间
        /// Unity屏幕空间左下角为(0,0)
        /// </summary>
        public static Matrix4x4 CanonicalCube2ScreenMatrix(float width,float height)
        {
            return new Matrix4x4(
                new Vector4(width / 2, 0, 0, 0),
                new Vector4(0, height / 2, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(width / 2, height / 2, 0, 1));
        }
        
    }
}