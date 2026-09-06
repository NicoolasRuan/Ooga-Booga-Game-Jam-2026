using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform player;

    private float highestY;
    private float fixedX;

    private void Start()
    {
        highestY = transform.position.y;
        fixedX = transform.position.x;
    }


    private void LateUpdate()
    {
        if(!GameManager.Instance.isGameOver)
        {
            if(player.position.y > highestY)
            {
                highestY = player.position.y;
            }

            transform.position = new Vector3(fixedX, highestY, transform.position.z);
        }
    }
}
