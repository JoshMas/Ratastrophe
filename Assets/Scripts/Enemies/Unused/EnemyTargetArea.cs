using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyTargetType
{
    Vantage,
    Balanced,
    Personal
}

[RequireComponent(typeof(SphereCollider))]
public class EnemyTargetArea : MonoBehaviour
{

    private SphereCollider sphere;

    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
        sphere.isTrigger = true;
    }

    public Vector3 GetPoint()
    {
        return sphere.center + Random.insideUnitSphere * sphere.radius;
    }
}
