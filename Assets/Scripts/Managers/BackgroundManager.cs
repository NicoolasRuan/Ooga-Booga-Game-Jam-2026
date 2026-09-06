using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Transform camera_target;

    [SerializeField] GameObject[] backgrounds;

    [SerializeField] private float chunkHeight = 16f;
    [SerializeField] private int chunksAhead = 3;
    [SerializeField] private int chunksBehind = 2;


    private float nextSpawnY;

    private List<GameObject> backgroundSpawned = new List<GameObject>();

    private void Start()
    {
        nextSpawnY = 0f;

        for(int i = 0; i < 5;  i++)
        {
            SpawnBackground();
        }
    }

    private void Update()
    {
        GenerateBackgrounds();
        RemoveOldBackground();
    }

    void GenerateBackgrounds()
    {
        float generationLimit = camera_target.position.y + chunkHeight * chunksAhead;   
        
        while (nextSpawnY < generationLimit)
        {
            SpawnBackground();
        }
    }

    void RemoveOldBackground()
    {
        float destroyLimit = camera_target.position.y - chunkHeight * chunksBehind;

        for (int i = backgroundSpawned.Count - 1; i >= 0; i--)
        {
            if(backgroundSpawned[i].transform.position.y < destroyLimit)
            {
                Destroy(backgroundSpawned[i]);

                backgroundSpawned.RemoveAt(i);
            }
        }
    }

    void SpawnBackground()
    {
        int randomIndex = Random.Range(0, backgrounds.Length);

        GameObject new_background = Instantiate(backgrounds[randomIndex], new Vector3(0f, nextSpawnY, 0f), Quaternion.identity);

        backgroundSpawned.Add(new_background);
        nextSpawnY += chunkHeight;

    }
}
