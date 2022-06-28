using UnityEngine;

public static class Extensions
{
    public static Bounds GetCameraOrthographicBounds(this Camera camera)
    {
        if (camera.orthographic)
        {
            float cameraHeight = camera.orthographicSize * 2;

            return new Bounds(
                camera.transform.position, 
                new Vector3(cameraHeight * camera.aspect, cameraHeight, 0));
        }
        else
        {
            Debug.LogError("Camera is not orthographic!", camera);
            return new Bounds();
        }
    }
}
