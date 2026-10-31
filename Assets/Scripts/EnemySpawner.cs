
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private List<GameObject> enemyPrefabs = new();

    [Header("Spawning Settings")]
    [SerializeField, Min(0.1f)] private float spawnCooldown = 3f;
    [SerializeField, Min(0.1f)] private float minimumSpawnCooldown = 0.5f;

    [Header("Difficulty Settings")]
    [SerializeField, Range(0.01f, 1f)]
    private float spawnCooldownReductionMultiplier = 0.99f;

    private readonly List<Vector3> spawnPositions = new();
    private float nextSpawnTime;

    private void Start()
    {
        // Check required references
        if (groundTilemap == null || enemyPrefabs.Count == 0)
        {
            Debug.LogError("EnemySpawner: Missing references!");
            enabled = false;
            return;
        }

        // Make sure every enemy prefab is assigned
        foreach (GameObject enemyPrefab in enemyPrefabs)
        {
            if (enemyPrefab == null)
            {
                Debug.LogError("EnemySpawner: An enemy prefab is missing!");
                enabled = false;
                return;
            }
        }

        // Find valid enemy spawn positions
        SetEnemySpawnPositions();

        if (spawnPositions.Count == 0)
        {
            Debug.LogError("EnemySpawner: No valid spawn positions!");
            enabled = false;
            return;
        }

        // Schedule the first enemy spawn
        nextSpawnTime = Time.time + spawnCooldown;

        // Increase difficulty once every second
        InvokeRepeating(
            nameof(HandleGameDifficultyIncrease),
            1f,
            1f
        );
    }

    private void Update()
    {
        // Stop spawning while the game is paused
        if (Time.timeScale == 0f)
            return;

        HandleEnemySpawning();
    }

    private void SetEnemySpawnPositions()
    {
        spawnPositions.Clear();

        // Search all cells in the Ground Tilemap
        foreach (Vector3Int cellPosition in
                 groundTilemap.cellBounds.allPositionsWithin)
        {
            if (groundTilemap.HasTile(cellPosition))
            {
                Vector3 worldPosition =
                    groundTilemap.GetCellCenterWorld(cellPosition);

                spawnPositions.Add(worldPosition);
            }
        }

        Debug.Log(
            $"EnemySpawner: Found {spawnPositions.Count} spawn positions."
        );
    }

    private void HandleEnemySpawning()
    {
        if (Time.time < nextSpawnTime)
            return;

        SpawnEnemyToRandomLocation();

        // Schedule the next enemy spawn
        nextSpawnTime = Time.time + spawnCooldown;
    }

    private void SpawnEnemyToRandomLocation()
    {
        // Pick a random ground position
        int randomPositionIndex =
            Random.Range(0, spawnPositions.Count);

        Vector3 spawnPosition =
            spawnPositions[randomPositionIndex];

        // Pick a random enemy type
        int randomEnemyIndex =
            Random.Range(0, enemyPrefabs.Count);

        GameObject selectedEnemy =
            enemyPrefabs[randomEnemyIndex];

        // Spawn the selected enemy
        Instantiate(
            selectedEnemy,
            spawnPosition,
            Quaternion.identity
        );
    }

    private void HandleGameDifficultyIncrease()
    {
        // Gradually reduce the spawn cooldown
        spawnCooldown *= spawnCooldownReductionMultiplier;

        // Keep the spawn rate under control
        spawnCooldown = Mathf.Max(
            spawnCooldown,
            minimumSpawnCooldown
        );
    }
}
