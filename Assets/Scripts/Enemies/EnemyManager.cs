using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrystalColour
{
    Red,
    Blue,
    Green
}

[System.Serializable]
public class ColourMaterial
{
    public CrystalColour colour;
    public Material material;
}

public class EnemyManager : MonoBehaviour
{
    #region singleton
    public static EnemyManager Instance;
    private void Singleton()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public ColourMaterial[] colours;

    [SerializeField]
    private List<Wave> waves;

    private List<Transform> spawnPoints;

    [HideInInspector]
    public List<GameObject> ActiveEnemies;

    private void Awake()
    {
        Singleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new List<Transform>();
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach(Transform child in children)
        {
            spawnPoints.Add(child);
        }
        spawnPoints.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(ActiveEnemies.Count == 0)
        {
            SpawnWave();
        }

    }

    public ColourMaterial GetRandomMaterial()
    {
        return colours[Random.Range(0, colours.Length)];
    }

    public ColourMaterial GetRandomMaterial(ColourMaterial _excluded)
    {
        ColourMaterial colMat = GetRandomMaterial();
        if (colMat.colour == _excluded.colour)
            return GetRandomMaterial(_excluded);
        else
            return colMat;
    }

    private void SpawnWave()
    {
        if(waves.Count == 0)
        {
            Win();
            return;
        }

        int spawnNum = waves[0].GetRandomSpawnCount();

        for (int i = 0; i < spawnNum; ++i)
        {
            ActiveEnemies.Add(Instantiate(waves[0].GetRandomEnemy(), GetRandomSpawn(), Quaternion.identity));
        }

        waves.RemoveAt(0);
    }

    private Vector3 GetRandomSpawn()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)].position + Random.insideUnitSphere;
    }

    private void Win()
    {
        Debug.Log("a");
        enabled = false;
    }
}
