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

    [SerializeField]
    private GameObject[] enemyTypes;

    private List<Transform> spawnPoints;

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
        
    }



    private void SpawnWave()
    {

    }
}
