using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    private float lifetime;
    public float Lifetime
    {
        get
        {
            return lifetime;
        }
        set
        {
            lifetime = value;
        }
    }

    public ColourMaterial Colour;

    private void Start()
    {
        GetComponentInChildren<MeshRenderer>().material = Colour.material;
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward, Space.Self);
        lifetime -= Time.deltaTime;
        if(lifetime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //take damage, add bullet to inventory, etc.
        }
        else
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.ColourMatches(Colour.colour))
            {
                enemy.TakeDamage();
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
