using UnityEngine;

public class CameraAspectRatio : MonoBehaviour {
    public Camera mainCamera;
    public Transform cameraPosition;
    public PathMaking pathMaking;

    public int heightRatio;
    public int widthRatio;
    public float cameraHeight;
    public float cameraLength;

    void Start() {
        mainCamera = GetComponent<Camera>();
        cameraPosition = GetComponent<Transform>();

        heightRatio = pathMaking.mapHeight;
        widthRatio = pathMaking.mapWidth;

        cameraHeight = -heightRatio * 10f / 2f + 5f;
        cameraLength = widthRatio * 10f / 2f - 5f;

        // positioning the camera to the middle of the tilemap
        cameraPosition.position = new Vector3(cameraLength, 20f, cameraHeight);

        // setting the camera aspect ratio to the tilemap size
        mainCamera.aspect = widthRatio / heightRatio;

        // stting the camera size
        mainCamera.orthographicSize = widthRatio * 5f;
    }
}
