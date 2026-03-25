using UnityEngine;

public class CameraAspectRatio : MonoBehaviour {
    public Camera mainCamera;
    public Transform cameraPosition;
    public PathMaking pathMaking;

    public float heightRatio;
    public float widthRatio;
    public float cameraHeight;
    public float cameraLength;

    void Start() {
        mainCamera = GetComponent<Camera>();
        cameraPosition = GetComponent<Transform>();

        float uiHeightAdjust = pathMaking.mapHeight * .278f;
        
        heightRatio = pathMaking.mapHeight + uiHeightAdjust;
        widthRatio = pathMaking.mapWidth;

        cameraHeight = -heightRatio * 5f / 2f + 2.5f + uiHeightAdjust * 5f;
        cameraLength = widthRatio * 5f / 2f - 5f;

        // positioning the camera to the middle of the tilemap
        cameraPosition.position = new Vector3(cameraLength, 20f, cameraHeight);

        // stting the camera size
        mainCamera.orthographicSize = heightRatio * 2.5f;
    }
}
