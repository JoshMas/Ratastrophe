using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapon")]
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

    public GameObject bullet;
    [SerializeField]
    private float speed = 10;

    private void OnValidate()
    {
        if(bullet != null)
        {
            Bullet script = bullet.GetComponent<Bullet>();
            if(script != null)
            {
                script.Speed = speed;
            }
        }
    }

    public void FireBullet(Vector3 _position, Vector3 _direction)
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

        Instantiate(bullet, _position, Quaternion.Euler(direction));
    }
}
