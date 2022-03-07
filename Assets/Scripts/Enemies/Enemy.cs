using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{

    private NavMeshAgent agent;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float sightDistance = 10;

    private Transform player;

    [SerializeField]
    private float raycastDelay = .1f;
    private float raycastTimer = 0;

    private bool shooting = false;
    private float shotTimer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting)
        {
            Shoot();
        }

        raycastTimer += Time.deltaTime;
        if(raycastTimer > raycastDelay)
        {
            raycastTimer = 0;
            shooting = IsPlayerVisible();
            if (!shooting)
            {
                PathTowardsPlayer();
            }
        }
    }
    private bool IsPlayerVisible()
    {
        if(!Physics.Raycast(transform.position, GetDirectionToPlayer(), out RaycastHit hit, sightDistance))
        {
            return false;
        }
        return hit.transform.gameObject.CompareTag("Player");
    }

    private Vector3 GetDirectionToPlayer()
    {
        return transform.InverseTransformPoint(player.position).normalized;
    }

    private void Shoot()
    {
        if (agent.hasPath)
        {
            agent.ResetPath();
        }

        shotTimer += Time.deltaTime;
        if(shotTimer > weapon.FireDelay)
        {
            shotTimer = 0;
            weapon.FireBullet(transform.position, GetDirectionToPlayer());
        }
    }

    private void PathTowardsPlayer()
    {
        if (agent.hasPath)
        {
            return;
        }
        shotTimer = 0;
        agent.SetDestination(player.position);
    }

}
