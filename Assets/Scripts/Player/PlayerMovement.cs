using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float impulseForce = 5f;
    [SerializeField] private float maxDragDistance = 300f;

    [SerializeField] private float launchSpeed = 12f;

    [SerializeField] private int maxJumpCharges = 2;

    private int currentJumpCharges;

    [SerializeField] private Transform playerVisual; // fish's visual

    public LineRenderer lineRenderer;

    private Rigidbody2D rb;
    private Camera cam;

    public Vector2 startDrag;
    public Vector2 currentDrag;

    public bool dragging;

    public Transform cameraTarget;

    private float defaultFixedDeltaTime;


    void Awake()
    {
        //oi = new Gradient();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        lineRenderer.enabled = false;

        defaultFixedDeltaTime = Time.fixedDeltaTime;

        currentJumpCharges = maxJumpCharges;

        rb.gravityScale = 0f;

    }

    private void Start()
    {
        HUDManager.Instance.UpdateWaterCharges(currentJumpCharges);
    }

    void Update()
    {
        GameOver();
        if (Input.GetMouseButtonDown(0) && currentJumpCharges >= 1)
        {
            StartDrag();
        }

        if(Input.GetMouseButton(0) && dragging)
        {
            UpdateDrag();
        }

        if(Input.GetMouseButtonUp(0) && dragging)
        {
            ReleaseDrag();
        }
    }

    void GameOver()
    {
        float distance = Vector2.Distance(transform.position, cameraTarget.position);
        if(distance > 5)
        {
            Destroy(gameObject);
            GameManager.Instance.isGameOver = true;
        } else
        {
            GameManager.Instance.isGameOver = false;
        }
    }

    void StartDrag()
    {
        //Time.timeScale = 0.2f;
        SetTimeScale(0.2f);
        dragging = true;

        startDrag = Input.mousePosition;

        lineRenderer.enabled = true;    
    }

    void UpdateDrag()
    {
        currentDrag = Input.mousePosition;

        Vector2 drag = currentDrag - startDrag;

        //if (drag.magnitude < 5f)
        //    return;
        //drag = Vector2.ClampMagnitude(-drag.normalized, maxDragDistance); // limite distancia

        Vector2 launchDirection = -drag.normalized;

        RotatePlayer(launchDirection);

        ShowDirection(launchDirection);
    }

    void ReleaseDrag()
    {

        //Time.timeScale = 1f;
        SetTimeScale(1f);

        rb.gravityScale = 1.4f;

        if (currentJumpCharges <= 0)
        {
            dragging = false;
            lineRenderer.enabled = false;
            return;
        }
        currentDrag = Input.mousePosition;

        Vector2 drag = currentDrag - startDrag;

        float dragDistance = Mathf.Clamp(drag.magnitude, 0, maxDragDistance);

        //drag = Vector2.ClampMagnitude(drag, maxDragDistance);

        float forcePorcentage = dragDistance / maxDragDistance;

        Vector2 launchDirection = -drag.normalized;

        RotatePlayer(launchDirection);

        //float forcePercentage = dragDistance / maxDragDistance;

        //rb.linearVelocity = Vector2.zero; // zera a vel anterior

        rb.linearVelocity = launchDirection * launchSpeed * forcePorcentage;

        //rb.AddForce(launchDirection * dragDistance * impulseForce, ForceMode2D.Impulse);


        currentJumpCharges--;
        HUDManager.Instance.UpdateWaterCharges(currentJumpCharges);

        dragging = false;

        lineRenderer.enabled = false;
    }

    void RotatePlayer(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        playerVisual.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    void ShowDirection(Vector2 direction)
    {
        Vector2 start = transform.position;
        Vector2 end = start + direction * 2f;

        //lineRenderer.startColor = Color.black;
        //lineRenderer.endColor = Color.black;
        lineRenderer.SetPosition(0, start);

        //Vector2 endPos = (Vector2)transform.position + direction;

        lineRenderer.SetPosition(1, end);
    }

    private Vector2 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition; // pega a pos do mouse

        mousePos.z = -cam.transform.position.z;

        return cam.ScreenToWorldPoint(mousePos);
    }

    //void CheckScreenWrap()
    //{
    //    Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);

    //    if(viewportPos.x < 0f)
    //    {
    //        Vector3 newPos = cam.ViewportToWorldPoint(new Vector3(1f, viewportPos.y, viewportPos.z));

    //        transform.position = new Vector3(
    //            newPos.x,
    //            transform.position.y,
    //            transform.position.z
    //        );
    //    }
    //}

    void IncreaseJump()
    {
        currentJumpCharges++;
        if(currentJumpCharges > 2)
        {
            currentJumpCharges = 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            //Debug.Log("OSADKOASKOD");
            IncreaseJump();
            HUDManager.Instance.UpdateWaterCharges(currentJumpCharges);
            Destroy(collision.gameObject);
        }
    }

    void SetTimeScale(float scale)
    {
        Time.timeScale = scale;

        Time.fixedDeltaTime =
            defaultFixedDeltaTime * scale;
    }
}
