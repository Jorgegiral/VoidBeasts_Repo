using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    [Header("Prefab Enemigos")]
    [SerializeField] GameObject enemyBasic;
    [SerializeField] GameObject enemyPuñetero;
    [Header("Referencias")]
    [SerializeField] TMP_Text enemyText;
    [SerializeField] Transform MainBuild;
    [Header("Opciones de oleadas")]
    [SerializeField] int enemyQuantity;
    [SerializeField] private float waveCD = 2f;

    [Header("Spawners")]
    [SerializeField] List<Transform> spawnersTransform = new List<Transform>();
    private int enemyCount;
    private float waveTimer;
    private int enemyRemain;
    private bool spawning = true;
    private bool finishedNight = false;
    private Transform selectOne;
    [SerializeField] GameObject warningVFX;
    void Update()
    {

        if (DayNightSystem.Instance.isNight && !spawning)
        {
            spawning = true;
            enemyQuantity = DayNightSystem.Instance.enemyQuantity;
            enemyRemain = DayNightSystem.Instance.enemyQuantity;
            waveTimer = 0f;
        }
        if (DayNightSystem.Instance.isNight && spawning)
        {
            waveTimer += Time.deltaTime;

            if (waveTimer >= waveCD && enemyRemain > 0)
            {
                finishedNight = true;
                waveTimer = 0f; 
                if (DayNightSystem.Instance.nightNumber >= 5)
                { 
                SpawnEnemies(SelectOneFromTheList());
                }
                else
                {
                    SpawnEnemies(selectOne);
                }

        }
        }
        if (DayNightSystem.Instance.isDay && spawning)
        {      
            selectOne = SelectOneFromTheList();
            spawning = false;
            enemyRemain = 0;
        }
        if (DayNightSystem.Instance.isNight)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            enemyText.text = enemyCount.ToString();
            if (enemyRemain == 0 && enemyCount == 0 && finishedNight)
            {
                DayNightSystem.Instance.ToDay();
                finishedNight = false;
            }
        }

    }
    private void SpawnEnemies(Transform spawnerSelected)
    {

        if (enemyRemain > 0) {
            int enemiesThisWave = Random.Range(1, enemyRemain/4);
            enemyRemain -= enemiesThisWave;
            for (int i = 0; i < enemiesThisWave; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 3f;
            Vector3 spawnPos = spawnerSelected.position + new Vector3(0, 1, 0);

                GameObject prefabToSpawn = ChooseEnemyType();

                Instantiate(prefabToSpawn, spawnPos, Quaternion.LookRotation(MainBuild.position- spawnPos));

        }
    }

}
    private GameObject ChooseEnemyType()
    {
        int night = DayNightSystem.Instance.nightNumber;

        float chancePuñetero = 0.35f; 

    
        float roll = Random.value; 
        return roll < chancePuñetero ? enemyPuñetero : enemyBasic;
    }

    private Transform SelectOneFromTheList()
    {
        return spawnersTransform[Random.Range(0, spawnersTransform.Count)];
    }
}

