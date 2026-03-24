using UnityEngine;

public class CameraAspectRatio : MonoBehaviour {
    public Camera mainCamera;
    public Transform cameraPosition;
    public PathMaking pathMaking;

    public float heightRatio;
    public float widthRatio;
    public float cameraHeight;
    public float cameraLength;

    void Awake() {
        mainCamera = GetComponent<Camera>();
        cameraPosition = GetComponent<Transform>();

        heightRatio = pathMaking.mapHeight;
        widthRatio = pathMaking.mapWidth;

        cameraHeight = -heightRatio * 10f / 2f + 15f;
        cameraLength = widthRatio * 10f / 2f - 5f;

        // positioning the camera to the middle of the tilemap
        cameraPosition.position = new Vector3(cameraLength, 20f, cameraHeight);

        // setting the camera aspect ratio to the tilemap size
        mainCamera.aspect = widthRatio / heightRatio;

        // stting the camera size
        mainCamera.orthographicSize = heightRatio * 5f;
    }
}
