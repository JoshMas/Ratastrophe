using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private ColourMaterial bodyColour;
    private ColourMaterial weaponColour;
    [SerializeField]
    private bool weaponColourMatches = true;
    [SerializeField]
    private MeshRenderer materialToReplace;

    private NavMeshAgent agent;

    [SerializeField]
    private int health = 1;
    [SerializeField]
    private float speed = 1;

    [Header("Shooting")]
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private float sightDistance = 10;

    private Transform player;

    [SerializeField]
    private float raycastDelay = .1f;
    private float raycastTimer = 0;

    private bool shooting = false;
    private float shotTimer;
    private bool waitingForPath = false;

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
        SetColour();
        materialToReplace.material = bodyColour.material;
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 1)
        {
            return;
        }

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

    private void SetColour()
    {
        bodyColour = EnemyManager.Instance.GetRandomMaterial();
        if (weaponColourMatches)
        {
            weaponColour = bodyColour;
        }
        else
        {
            weaponColour = EnemyManager.Instance.GetRandomMaterial(bodyColour);
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
        return (player.position - transform.position).normalized;
    }

    private void Shoot()
    {
        agent.ResetPath();
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z), Vector3.up);
        shotTimer += Time.deltaTime;
        if(shotTimer > weapon.FireDelay)
        {
            shotTimer = 0;
            weapon.FireBullet(transform.position + transform.forward, GetDirectionToPlayer(), weaponColour);
        }
    }

    private void PathTowardsPlayer()
    {
        if (agent.hasPath || waitingForPath)
        {
            return;
        }
        shotTimer = 0;
        StartCoroutine(nameof(DelayPath));
    }

    private IEnumerator DelayPath()
    {
        waitingForPath = true;
        yield return new WaitForSeconds(Random.Range(0, 5));
        if (!shooting)
        {
            agent.SetDestination(player.position);
            waitingForPath = false;
        }
    }

    public bool ColourMatches(CrystalColour _colour)
    {
        return _colour == bodyColour.colour;
    }

    public void TakeDamage()
    {
        health -= 1;
        if(health < 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        EnemyManager.Instance.ActiveEnemies.Remove(gameObject);
    }
}
