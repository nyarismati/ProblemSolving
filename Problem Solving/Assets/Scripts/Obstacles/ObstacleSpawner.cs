using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : Spawner
{
    [SerializeField] private float spawnDelay = 6f;
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] private Collider2D edgesCollider;
    [SerializeField] private int difficultyIncrementScore = 20;

    private List<GameObject> spawnedObstacles = new List<GameObject>();
    private int lastScoreDifficultyIncrement = 0;

    float[,] edgesPosition;

    protected override void Start()
    {
        base.Start();

        edgesPosition = new float[,] { { edgesCollider.bounds.min.x, edgesCollider.bounds.max.x }, { edgesCollider.bounds.min.y, edgesCollider.bounds.max.y } };
        StartCoroutine(SpawnObstacles());
    }

    private void Update()
    {
        //if player have scored x amount of score, decrease the spawning delay
        int curScore = ScoreController.Instance.GetScore();
        if (curScore != lastScoreDifficultyIncrement && curScore % difficultyIncrementScore == 0)
        {
            spawnDelay = Mathf.Clamp(spawnDelay - 0.5f, 1f, 100f);
            lastScoreDifficultyIncrement = curScore;
        }
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            //randomize obstacle delay
            float randomTime = Random.Range(spawnDelay - 1f, spawnDelay + 1f);
            yield return new WaitForSeconds(randomTime);

            //get obstacle and set position
            GameObject newObstacle = GetOrCreateObstacle();
            SetObstacleSpawnPosition(newObstacle);
            newObstacle.SetActive(true);
        }
    }

    private GameObject GetOrCreateObstacle()
    {
        int randomObstacleIndex = Random.Range(0, obstaclesPrefabs.Length);

        //get obstacle from pool
        GameObject obstacle = spawnedObstacles.Find(o => !o.activeSelf && o.name.Contains(obstaclesPrefabs[randomObstacleIndex].name));

        //if there is none available, create it
        if(obstacle == null)
        {
            obstacle = Instantiate(obstaclesPrefabs[randomObstacleIndex]);
            spawnedObstacles.Add(obstacle);
        }

        return obstacle;
    }

    private void SetObstacleSpawnPosition(GameObject obstacle)
    {
        //missile obstacle is spawned
        if (obstacle.GetComponent<Missile>() != null)
        {
            Vector2 newPosition = GetRandomSpawnPosition();

            //determine on what edge will the missile spawn on, x or y, min or max
            int randNum = Random.Range(0, 2);
            if (randNum == 0)
                newPosition.x = edgesPosition[randNum, Random.Range(0, 2)];
            else
                newPosition.y = edgesPosition[randNum, Random.Range(0, 2)];

            obstacle.transform.position = newPosition;

        }
        else
        {
            obstacle.transform.position = GetRandomSpawnPosition();
        }
    }
}
