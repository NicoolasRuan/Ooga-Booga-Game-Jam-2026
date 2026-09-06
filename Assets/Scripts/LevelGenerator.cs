using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform cameraTarget;
    public GameObject[] easyChunks;
    public GameObject[] mediumChunks;
    public GameObject[] hardChunks;

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
        //int random_chunk = Random.Range(0, chunksPrefabs.Length);
        //Debug.Log(random_chunk);
        //Debug.Log(chunksPrefabs[random_chunk]);
        GameObject chunk_ = Instantiate(GetRandomChunk(), new Vector3(0f, nextSpawnY, 0f), Quaternion.identity);

        chunksSpawned.Add(chunk_);

        nextSpawnY += chunckHeight;
    }

    GameObject GetRandomChunk()
    {
        float playerHeight = cameraTarget.position.y;

        if(playerHeight < 100f)
        {
            return easyChunks[Random.Range(0, easyChunks.Length)];
        } 
        
        if(playerHeight < 200f)
        {
            return mediumChunks[Random.Range(0, mediumChunks.Length)];
        } 
        
        return hardChunks[Random.Range(0, hardChunks.Length)];
    }
}
