using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
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

    private LayerMask aimingMask;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        aimingMask = LayerMask.GetMask("Default");
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
        else
        {
            PathTowardsPlayer();
        }

        raycastTimer += Time.deltaTime;
        if(raycastTimer > raycastDelay)
        {
            raycastTimer = 0;
            shooting = IsPlayerVisible();
        }
    }
    private bool IsPlayerVisible()
    {
        if(!Physics.Raycast(transform.position, GetDirectionToPlayer(), out RaycastHit hit, sightDistance, aimingMask, QueryTriggerInteraction.Collide))
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
        agent.ResetPath();

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
