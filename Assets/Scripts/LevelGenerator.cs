using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform cameraTarget;
    public GameObject chunck;

    public float chunckHeight = 10f;
    public int chunksAhead = 5;// chunks para cima
    public int chunksBehind = 3;// chunks atras


    public float nextSpawnY; // altura do prox spawn

    public List<GameObject> chunksSpawned = new List<GameObject>();

    private void Start()
    {
        nextSpawnY = 0f;

        for (int i = 0; i < chunksAhead; i++)
        {
            SpawnChunk();
        }
    }

    private void Update()
    {
        GenerateChunk();
        RemoveOldChunks();
    }

    void GenerateChunk()
    {
        float generation_limit = cameraTarget.position.y + chunckHeight * chunksAhead;

        if(nextSpawnY < generation_limit)
        {
            SpawnChunk();
        }
    }

    void RemoveOldChunks()
    {
        float destroy_limit = cameraTarget.position.y - chunckHeight * chunksBehind;

        for(int i = chunksSpawned.Count - 1; i >= 0; i--)
        {
            if(chunksSpawned[i].transform.position.y < destroy_limit)
            {
                Destroy(chunksSpawned[i]);
                chunksSpawned.RemoveAt(i);
            }
        }
    }
    void SpawnChunk()
    {
        GameObject chunk_ = Instantiate(chunck, new Vector3(0f, nextSpawnY, 0f), Quaternion.identity);

        chunksSpawned.Add(chunk_);

        nextSpawnY += chunckHeight;
    }
}
