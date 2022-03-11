using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{

    [SerializeField]
    private float fireRate = 1;
    public float FireDelay
    {
        get
        {
            return 1 / fireRate;
        }
    }

    [SerializeField, Range(0, 1)]
    private float spread = 0.5f;

    [Header("Bullet Properties")]
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float lifetime = 5;

    public void FireBullet(Vector3 _position, Vector3 _direction, ColourMaterial _colour)
    {
        Vector3 direction = _direction + Random.onUnitSphere * spread;
        if(direction.magnitude == 0)
        {
            direction = _direction;
        }
        else
        {
            direction = direction.normalized;
        }

        Bullet newBullet = Instantiate(bullet, _position, Quaternion.LookRotation(direction)).GetComponent<Bullet>();
        newBullet.Speed = speed;
        newBullet.Lifetime = lifetime;
        newBullet.Colour = _colour;
    }
}
