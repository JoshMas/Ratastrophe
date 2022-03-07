using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{

    private NavMeshAgent agent;

    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float sightDistance = 10;

    private Transform player;

    [SerializeField]
    private float raycastDelay = .1f;
    private float raycastTimer = 0;

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
        raycastTimer += Time.deltaTime;
        if(raycastTimer > raycastDelay)
        {
            raycastTimer = 0;

            if (IsPlayerVisible())
            {
                Shoot();
            }
            else
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

    }

    private void PathTowardsPlayer()
    {
        if (agent.hasPath)
        {
            return;
        }
        agent.SetDestination(player.position);
    }

}
