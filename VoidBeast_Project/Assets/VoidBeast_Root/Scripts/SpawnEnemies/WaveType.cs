using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveType", menuName = "Scriptable Objects/WaveType")]
public class WaveType : ScriptableObject
{
        [Header("Rango de noches")]
        public int minNight;
        public int maxNight;

        [Header("Ritmo")]
        public float timeBetweenSpawns = 1f;

        [Header("Enemigos y probabilidades")]
        public List<EnemyChance> enemies;

        [Header("Spawners")]
        public int maxSpawnersUsed = 2;
}
[System.Serializable]
public class EnemyChance
{
    public GameObject enemyPrefab;
    [Range(0f, 1f)] public float chance;
    public int weight = 1;
}