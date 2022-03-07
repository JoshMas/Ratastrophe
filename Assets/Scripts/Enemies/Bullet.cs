using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward, Space.Self);
    }
}
