using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float impulseForce = 5f;
    [SerializeField] private float maxDragDistance = 3f;

    public LineRenderer lineRenderer;

    private Rigidbody2D rb;
    private Camera cam;

    public Vector2 startDrag;
    public Vector2 currentDrag;

    public bool dragging;


    //public Gradient oi;
    void Awake()
    {
        //oi = new Gradient();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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

    void StartDrag()
    {
        Time.timeScale = 0.2f;
        dragging = true;

        startDrag = Input.mousePosition;

        lineRenderer.enabled = true;    
    }

    void UpdateDrag()
    {
        currentDrag = Input.mousePosition;

        Vector2 drag = currentDrag - startDrag;

        drag = Vector2.ClampMagnitude(-drag.normalized, maxDragDistance); // limite distancia



        ShowDirection(drag);
    }

    void ReleaseDrag()
    {

        Time.timeScale = 1f;
        currentDrag = Input.mousePosition;

        Vector2 drag = currentDrag - startDrag;

        float dragDistance = Mathf.Clamp(drag.magnitude, 0, maxDragDistance);

        //drag = Vector2.ClampMagnitude(drag, maxDragDistance);

        Vector2 launchDirection = -drag.normalized;

        rb.linearVelocity = Vector2.zero; // zera a vel anterior

        rb.AddForce(launchDirection * dragDistance * impulseForce, ForceMode2D.Impulse);

        dragging = false;

        lineRenderer.enabled = false;
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
}
