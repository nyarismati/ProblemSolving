using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private int maxSquareCount = 5;
    private Collider2D spawnCollider;

    private Vector2 maxSpawningPosition;
    private Vector2 minSpawningPosition;

    private void Start()
    {
        spawnCollider = GetComponent<Collider2D>();

        maxSpawningPosition = spawnCollider.bounds.max;
        minSpawningPosition = spawnCollider.bounds.min;

        SpawnSquares();
    }

    private void SpawnSquares()
    {
        float squareSpawnCount = Random.Range(1, maxSquareCount + 1);
        for (int i = 0; i < squareSpawnCount; i++)
        {
            GameObject newSquare = Instantiate(squarePrefab, squarePrefab.transform.position, Quaternion.identity);

            newSquare.transform.position = GetRandomSpawnPosition();
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(minSpawningPosition.x, maxSpawningPosition.x);
        float randomY = Random.Range(minSpawningPosition.y, maxSpawningPosition.y);
        return new Vector2(randomX, randomY);
    }
}
