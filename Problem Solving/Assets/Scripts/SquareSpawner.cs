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

    public bool SquareCanRespawn;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

            //set action delegate function setelah square dicollect
            if (SquareCanRespawn)
            {
                SquareController squareController = newSquare.GetComponent<SquareController>();
                squareController.OnSquareDestroyed += RespawnSquare;
            }

            //randomize its position
            SetRandomSquarePosition(newSquare);
        }
    }

    public void RespawnSquare(SquareController square)
    {
        StartCoroutine(RespawnSquareAfterSeconds(square));
    }

    private IEnumerator RespawnSquareAfterSeconds(SquareController square)
    {
        yield return new WaitForSeconds(3f);
        square.gameObject.SetActive(true);
        SetRandomSquarePosition(square.gameObject);
    }

    private void SetRandomSquarePosition(GameObject squareGO)
    {
        Vector2 randomSquarePosition = GetRandomSpawnPosition();

        float playerRadius = player.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        SpriteRenderer squareRenderer = squareGO.GetComponent<SpriteRenderer>();
        float squareDiagonal = Mathf.Sqrt(Mathf.Pow(squareRenderer.bounds.extents.x, 2) + Mathf.Pow(squareRenderer.bounds.extents.y, 2));

        while (Vector2.Distance(player.position, randomSquarePosition) < playerRadius + squareDiagonal)
        {
            Debug.Log(Vector2.Distance(player.position, randomSquarePosition));
            randomSquarePosition = GetRandomSpawnPosition();
        }

        squareGO.transform.position = randomSquarePosition;
    }

    private Vector2 GetRandomSpawnPosition()
    {
        //randomize position from min & max spawning position
        float randomX = Random.Range(minSpawningPosition.x, maxSpawningPosition.x);
        float randomY = Random.Range(minSpawningPosition.y, maxSpawningPosition.y);
        return new Vector2(randomX, randomY);
    }
}
