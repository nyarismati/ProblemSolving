using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    protected Collider2D spawnCollider;

    protected Vector2 maxSpawningPosition;
    protected Vector2 minSpawningPosition;

    protected virtual void Start()
    {
        spawnCollider = GetComponent<Collider2D>();

        //get max & min spawning position from collider bounds
        maxSpawningPosition = spawnCollider.bounds.max;
        minSpawningPosition = spawnCollider.bounds.min;
    }

    protected Vector2 GetRandomSpawnPosition()
    {
        //randomize position from min & max spawning position
        float randomX = Random.Range(minSpawningPosition.x, maxSpawningPosition.x);
        float randomY = Random.Range(minSpawningPosition.y, maxSpawningPosition.y);
        return new Vector2(randomX, randomY);
    }
}
