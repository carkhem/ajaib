using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Controller
{
    [Header("Stats")]
    public float damage;
    public float maxHealth = 100;
    public int expGain = 50;
    //	[HideInInspector]
    public float health;
    [Header("Sight")]
    public GameObject eyes;
    public Vector3 offset;
    [Range(0.0f, 180.0f)]
    public float fov;
    public float viewDistance;
    public float detectionSpeed;
    public float detection; //Kan användas för att visa på hur nära man är på att bli upptäckt
    private float detectionTimer = 0;
    [HideInInspector]
    public Transform player;
    [Header("Movement")]
    public Animator anim;
    [HideInInspector]
    public string recentState;
    [HideInInspector]
    public float recentHealth;
	[HideInInspector]
	public bool dead = false;
    public Vector3[] patrolPoints;

    void Start()
    {
        player = PlayerStats.instance.transform;
        health = maxHealth;
    }

    public bool InSight(Transform thing)
    {
        Vector3 direction = thing.position - eyes.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, direction, out hit, viewDistance))
        {
            if (hit.transform.CompareTag(thing.tag))
            {
                if (Vector3.Angle(eyes.transform.forward, direction) < fov)
                {
                    Debug.DrawRay(eyes.transform.position, direction, Color.green);
                    return true;
                } else {
                    Debug.DrawRay(eyes.transform.position, direction, Color.red);
                    return false;
                }
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
		anim.SetTrigger("hit");
        if (health <= 0){
            TransitionTo<DeadState>();
            PlayerStats.instance.AddExperience(expGain);
        }
        if (detection != 1){
            DetectPlayer();
//            TransitionTo<StunnedState>();
        }
    }

    public void LookForPlayer()
    {
        Debug.DrawRay(eyes.transform.position, Quaternion.AngleAxis(fov, eyes.transform.up) * eyes.transform.forward * viewDistance);
        Debug.DrawRay(eyes.transform.position, Quaternion.AngleAxis(-fov, eyes.transform.up) * eyes.transform.forward * viewDistance);

        //		detectionMultiplier = Vector3.Distance (transform.position, player.position) * 8;
        //		detectionMultiplier = 5 / Vector3.Angle (transform.forward, player.position - transform.position);

        if (InSight(player))
        {
            if (detectionTimer < detectionSpeed)
                detectionTimer += Time.deltaTime / Vector3.Distance(eyes.transform.position, player.position) * 8;
            else
                detectionTimer = detectionSpeed;
        }
        else
        {
            if (detectionTimer > 0)
                detectionTimer -= Time.deltaTime;
            else
                detectionTimer = 0;
        }
        detection = detectionTimer / detectionSpeed;
        if (detection >= 1){
            DetectPlayer();
        }
    }

    //DEN HÄR FUNKAR INTE ALLTID AV NÅGON ANLEDNING
    public bool SamePosition(Vector3 posOne, Vector3 posTwo)
    {
        if ((int)posOne.x == (int)posTwo.x && (int)posOne.z == (int)posTwo.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsMoving(NavMeshAgent agent)
    {
        if (agent.velocity.x == 0 && agent.velocity.z == 0)
            return false;
        else
            return true;
    }

    public void SetAnim(string boolName, bool condition)
    {
        anim.SetBool(boolName, condition);
    }

    public float GetHealthPercentage()
    {
        return health / maxHealth;
    }

    public void DetectPlayer()
    {
        detection = 1;
        detectionTimer = 0;
		if (health > 0)
	        player.GetComponent<PlayerStats>().AddEnemy(gameObject);
		TransitionTo<CombatState> ();
    }

    public void Revive()
    {
        health = maxHealth;
        TransitionTo<CombatState>();
    }

    public void Respawn()
    {

        TransitionTo<IdleState>();
        detection = 0f;
        transform.position = patrolPoints[0];
        GetComponent<NavMeshAgent>().SetDestination(patrolPoints[0]);

    }


}
