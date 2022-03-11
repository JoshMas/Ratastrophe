using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyWeight
{
    public GameObject enemy;
    [Min(1)] public int weight;

    public void AddEnemies(ref List<GameObject> _list)
    {
        for (int i = 0; i < weight; ++i)
        {
            _list.Add(enemy);
        }
    }
}

[CreateAssetMenu(menuName = "ScriptableObjects/Wave")]
public class Wave : ScriptableObject
{
    [SerializeField]
    private EnemyWeight[] table;
    private List<GameObject> enemies;

    [SerializeField, Min(0)] private int min = 1;
    [SerializeField, Min(1)] private int max = 3;

    private void OnEnable()
    {
        if (table == null)
            return;

        enemies = new List<GameObject>();
        foreach(EnemyWeight pair in table)
        {
            pair.AddEnemies(ref enemies);
        }
    }

    public GameObject GetRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Count)];
    }

    public int GetRandomSpawnCount()
    {
        return Random.Range(min, max + 1);
    }
}
