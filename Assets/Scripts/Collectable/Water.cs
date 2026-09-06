using UnityEngine;

public class Water : MonoBehaviour
{

    [SerializeField] private float speed = 2f;

    private Vector2 leftPoint;
    private Vector2 rightPoint;

    private Vector2 target;



    public void Setup(Vector2 left, Vector2 right, bool startFromLeft)
    {
        leftPoint = left;
        rightPoint = right;

        target = startFromLeft ? rightPoint : leftPoint;
    }


    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);


        if(Vector2.Distance(transform.position, target) < 0.05f)
        {
            if(target == rightPoint)
            {
                target = leftPoint;
            } else
            {
                target = rightPoint;
            }
        }
    }
}
