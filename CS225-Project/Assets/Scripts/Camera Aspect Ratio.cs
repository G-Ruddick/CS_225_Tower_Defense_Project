using UnityEngine;

public class CameraAspectRatio : MonoBehaviour {
    public Camera mainCamera;
    public Transform cameraPosition;
    public PathMaking pathMaking;

    public float heightRatio;
    public float widthRatio;
    public float cameraHeight;
    public float cameraLength;

    void Update() {
        mainCamera = GetComponent<Camera>();
        cameraPosition = GetComponent<Transform>();

        float uiHeightAdjust = pathMaking.mapHeight * 0.278f;
        
        heightRatio = pathMaking.mapHeight + uiHeightAdjust;
        widthRatio = pathMaking.mapWidth;

        cameraHeight = -heightRatio * 10f / 2f + 5f + uiHeightAdjust * 10f;
        cameraLength = widthRatio * 10f / 2f - 5f;

        // positioning the camera to the middle of the tilemap
        cameraPosition.position = new Vector3(cameraLength, 20f, cameraHeight);

        // setting the camera aspect ratio to the tilemap size
        // mainCamera.aspect = widthRatio / heightRatio;

        // stting the camera size
        mainCamera.orthographicSize = heightRatio * 5f;
    }
}
