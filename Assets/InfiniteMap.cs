using UnityEngine;
using System.Collections.Generic;
using UnityEngine;


public class RandomInfiniteMap : MonoBehaviour
{
    public Transform player;
    public int tileSize = 10;
    public int viewDistance = 3;

    private Vector2Int currentPlayerTile;
    private Dictionary<Vector2Int, GameObject> spawnedTiles = new Dictionary<Vector2Int, GameObject>();
    public GameObject[] tilePrefabs;

   

    void Update()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.z);
        Vector2Int playerTile = new Vector2Int(
            Mathf.FloorToInt(playerPos.x / tileSize),
            Mathf.FloorToInt(playerPos.y / tileSize)
        );

        if (playerTile != currentPlayerTile)
        {
            currentPlayerTile = playerTile;
            UpdateVisibleTiles();
        }
    }

    void UpdateVisibleTiles()
    {
        HashSet<Vector2Int> newVisible = new HashSet<Vector2Int>();

        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int y = -viewDistance; y <= viewDistance; y++)
            {
                Vector2Int coord = new Vector2Int(currentPlayerTile.x + x, currentPlayerTile.y + y);
                newVisible.Add(coord);

                if (!spawnedTiles.ContainsKey(coord))
                {
                    SpawnTile(coord);
                }
            }
        }

        // Удаление старых тайлов
        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (var coord in spawnedTiles.Keys)
        {
            if (!newVisible.Contains(coord))
            {
                Destroy(spawnedTiles[coord]);
                toRemove.Add(coord);
            }
        }

        foreach (var coord in toRemove)
        {
            spawnedTiles.Remove(coord);
        }
    }

    void SpawnTile(Vector2Int coord)
    {
        Vector3 pos = new Vector3(coord.x * tileSize, 0, coord.y * tileSize);

        // Выбор случайного префаба
        GameObject prefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];

        GameObject tile = Instantiate(prefab, pos, Quaternion.identity);
        spawnedTiles.Add(coord, tile);
    }
}

