using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;

    private new Camera camera;
    private new Transform transform;

    private Vector3 startInputPosition;

    private void Awake()
    {
        camera = Camera.main;
        transform = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startInputPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 newInputPosition = camera.ScreenToViewportPoint(Input.mousePosition);
            Vector3 offset = (startInputPosition - newInputPosition) * cameraSpeed * Time.deltaTime;

            Vector3 newPosition = transform.localPosition + Vector3.right * offset.x + Vector3.up * offset.y;
            newPosition.x = Mathf.Clamp(newPosition.x, -30.0f, 30.0f);
            newPosition.y = Mathf.Clamp(newPosition.y, 0.0f, 30.0f);

            transform.localPosition = newPosition;
            startInputPosition = newInputPosition;
        }
    }
}
