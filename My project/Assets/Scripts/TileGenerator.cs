using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public float speed;


    public GameObject[] tilePrefabs;

    private float zSpawnPosition = 0;
    private float tileLength = 30;

    [SerializeField] private Transform player;

    private int startTiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();

    private void Start()
    {
        SpawnTile(0);
        SpawnTile(1);
        SpawnTile(2);
        SpawnTile(3);
        SpawnTile(4);


        for (int i = 0; i < startTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
      
    }

    private void Update()
    {
        if (player.position.z - 20 > zSpawnPosition - (startTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    private void FixedUpdate()
    {
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawnPosition, transform.rotation);
        activeTiles.Add(nextTile);
        zSpawnPosition += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}

