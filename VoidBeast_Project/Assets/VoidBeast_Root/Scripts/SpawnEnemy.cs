using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    [SerializeField] GameObject enemyBasic;
    [SerializeField] GameObject enemyPuñetero;

    [SerializeField] int enemyQuantity;
    [SerializeField] TMP_Text enemyText;
    [SerializeField] Transform MainBuild;
    [SerializeField] List<Transform> spawnersTransform = new List<Transform>();
    private int enemyCount;
    private float waveCD = 5f;
    private float waveTimer;
    private int enemyRemain;
    private bool spawning = true;
    private bool finishedNight = false;
    private Transform selectOne;

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
            if (enemyCount == 0 && finishedNight)
            {
                DayNightSystem.Instance.ToDay();
                finishedNight = false;
            }
        }

    }
    private void SpawnEnemies(Transform spawnerSelected)
    {

        if (enemyRemain > 0) {
            int enemiesThisWave = Random.Range(1, enemyRemain);
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

        float chancePuñetero = 0.2f; 

    
        float roll = Random.value; 
        return roll < chancePuñetero ? enemyPuñetero : enemyBasic;
    }

    private Transform SelectOneFromTheList()
    {
        return spawnersTransform[Random.Range(0, spawnersTransform.Count)];
    }
}

