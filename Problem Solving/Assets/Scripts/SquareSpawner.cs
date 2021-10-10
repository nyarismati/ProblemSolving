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

    [SerializeField] private Vector2 minSquareScale;
    [SerializeField] private Vector2 maxSquareScale;

    private void Start()
    {
        spawnCollider = GetComponent<Collider2D>();

        //get max & min spawning position from collider bounds
        maxSpawningPosition = spawnCollider.bounds.max;
        minSpawningPosition = spawnCollider.bounds.min;

        SpawnSquares();
    }

    private void SpawnSquares()
    {
        //randomize spawned square count
        float squareSpawnCount = Random.Range(1, maxSquareCount + 1);
        for (int i = 0; i < squareSpawnCount; i++)
        {
            //create new square
            GameObject newSquare = Instantiate(squarePrefab, squarePrefab.transform.position, Quaternion.identity);
            newSquare.transform.localScale = new Vector3(Random.Range(minSquareScale.x, maxSquareScale.x), Random.Range(minSquareScale.y, maxSquareScale.y), 1f);

            //randomize its position
            newSquare.transform.position = GetRandomSpawnPosition();
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        //randomize position from min & max spawning position
        float randomX = Random.Range(minSpawningPosition.x, maxSpawningPosition.x);
        float randomY = Random.Range(minSpawningPosition.y, maxSpawningPosition.y);
        return new Vector2(randomX, randomY);
    }
}
