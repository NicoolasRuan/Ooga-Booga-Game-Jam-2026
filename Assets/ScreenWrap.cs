using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform visual;
    [SerializeField] private Transform wrapVisual;

    private Rigidbody2D rb;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        wrapVisual.gameObject.SetActive(false);
    }

    private void Update()
    {
        HandleWrapVisual();
    }

    private void FixedUpdate()
    {
        HandleWrap();
    }


    void HandleWrapVisual() 
    {
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;


        float leftEdge = mainCamera.transform.position.x - cameraWidth / 2f;
        float rightEdge = mainCamera.transform.position.x + cameraWidth / 2f;

        float playerX = transform.position.x;

        if(playerX > rightEdge - 1f)
        {
            wrapVisual.gameObject.SetActive(true);

            wrapVisual.position = new Vector3(visual.position.x - cameraWidth, visual.position.y, visual.position.z);
        }

        else if (playerX < leftEdge + 1f)
        {
            wrapVisual.gameObject.SetActive(true);

            wrapVisual.position = new Vector3(visual.position.x - cameraWidth, visual.position.y, visual.position.z);
        }

        else
        {
            wrapVisual.gameObject.SetActive(false);
        }
    }

    void HandleWrap()
    {
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;


        float leftEdge = mainCamera.transform.position.x - cameraWidth / 2f;
        float rightEdge = mainCamera.transform.position.x + cameraWidth / 2f;

        Vector2 position = rb.position;

        if (position.x > rightEdge)
        {
            position.x -= cameraWidth;
            rb.position = position;
        }

        else if (position.x < leftEdge)
        {
            position.x += cameraWidth;
            rb.position = position;
        }
    }
}
