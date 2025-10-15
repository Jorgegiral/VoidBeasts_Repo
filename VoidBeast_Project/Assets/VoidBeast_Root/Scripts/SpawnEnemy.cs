using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform spawnerTransform;
    [SerializeField] int enemyQuantity;
    [SerializeField] TMP_Text enemyText;
    [SerializeField] float spawnRadius = 300f;
    private int maxGroupSize;
    private bool enemiesSpawned = false;
    private List<GameObject> activeEnemies = new List<GameObject>();



    void Update()
    {
        if (DayNightSystem.Instance == null)
            return;

        if (DayNightSystem.Instance.isNight && !enemiesSpawned)
        {
            enemyQuantity = DayNightSystem.Instance.enemyQuantity;
            SpawnEnemies();
            enemiesSpawned = true;
        }
        if (DayNightSystem.Instance.isDay && enemiesSpawned)
        {
            enemiesSpawned = false;
        }
        if (DayNightSystem.Instance.isNight)
        {
            int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            enemyText.text = "Enemigos: " + enemyCount;
            if (enemyCount == 0) { DayNightSystem.Instance.ToDay(); }
        }

    }
    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyQuantity; i++)
        {
            float angle = i * Mathf.PI * 2f / enemyQuantity;
            float x = Mathf.Cos(angle) * spawnRadius;
            float z = Mathf.Sin(angle) * spawnRadius;
            Vector3 spawnPos = spawnerTransform.position + new Vector3(x, 0, z);
            Instantiate(enemyPrefab, spawnPos, Quaternion.LookRotation(spawnerTransform.position - spawnPos));

        }


    }
}
