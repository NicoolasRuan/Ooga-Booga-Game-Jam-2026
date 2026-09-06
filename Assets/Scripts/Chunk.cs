using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private GameObject waterCharge;
    [SerializeField] private BoxCollider2D[] spawnAreas;

    private void Start()
    {
        SpawnWaterCharges();
    }

    void SpawnWaterCharges()
    {
        int randomArea = Random.Range(0, spawnAreas.Length);

        BoxCollider2D area = spawnAreas[randomArea];

        Bounds bounds = area.bounds;

        float randomY = Random.Range(bounds.min.y, bounds.max.y);


        bool startFromLeft = Random.value > 0.5f;

        Vector2 leftPosition = new Vector2(
            bounds.min.x,
            randomY
        );

        Vector2 rightPosition = new Vector2(
            bounds.max.x,
            randomY
        );

        Vector2 spawnPosition = startFromLeft ? leftPosition : rightPosition;

        GameObject water_charge = Instantiate(waterCharge, spawnPosition, Quaternion.identity, transform);

        water_charge.GetComponent<Water>().Setup(leftPosition,rightPosition,startFromLeft);

    }


}
