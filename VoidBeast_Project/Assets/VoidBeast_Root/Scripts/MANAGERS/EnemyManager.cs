using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
    public void Register(GameObject enemy)
    {
        enemies.Add(enemy);
    }
    public void UnRegister(GameObject enemy)
    {
        enemies.Remove(enemy);
        PlayerStats.instance.enemykilledCount++;
    }
}
