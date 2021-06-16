using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class Unit : MonoBehaviour
{
    [Header("Unit")]
    [SerializeField]
    GameObject hpBarPrefab;

    [SerializeField]
    float hp, hpMax = 100;
    public bool IsAlive { get { return hp > 0; } }
    public static List<ISeletable> SeletableUnit { get { return seletableUnit; } }

    [SerializeField]
    protected float stoppingDistance = 1, attacDistance = 1, attackCooldown = 1, attackDamage = 0;
    static List<ISeletable> seletableUnit = new List<ISeletable>();

    protected Animator animator;
    protected Transform targetToFollow;
    protected NavMeshAgent nav;
    protected HPBar healtBar;
    protected Task task = Task.idle;

    float attackTimer;

    public enum Task
    {
        idle, move, follow, chase, attack
    }

    public float HealthPrecent
    {
        get { return hp / hpMax; }
    }
    const string ANIMATOR_SPEED = "Speed",
        ANIMATOR_ALIVE = "Alive",
        ANIMATOR_ATTACK = "Basic Attack";


    protected virtual void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        hp = hpMax;
        healtBar = Instantiate(hpBarPrefab, transform).GetComponent<HPBar>();
    }

    protected virtual void Start()
    {
        if (this is ISeletable) seletableUnit.Add(this as ISeletable);
    }

    private void OnDestroy()
    {
        if (this is ISeletable) seletableUnit.Remove(this as ISeletable);
    }

    void Update()
    {
        if (IsAlive)
            switch (task)
            {
                case Task.idle: Idling(); break;
                case Task.move: Moving(); break;
                case Task.follow: Follwing(); break;
                case Task.chase: Chasing(); break;
                case Task.attack: Attacking(); break;
            }
        Animate();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        
    }


    protected virtual void Idling()
    {
        nav.velocity = Vector3.zero;
    }
    protected virtual void Attacking()
    {
        if (targetToFollow)
        {
            nav.velocity = Vector3.zero;
            transform.LookAt(targetToFollow);
            float distance = Vector3.Magnitude(targetToFollow.position - transform.position);
            if (distance < attacDistance)
            {
                if ((attackTimer -= Time.deltaTime) <= 0) Attack();
            }
            else
            {
                task = Task.chase;
            }
        }
        else
        {
            task = Task.idle;
        }
    }
    protected virtual void Moving()
    {
        float distance = Vector3.Magnitude(nav.destination - transform.position);
        if(distance<stoppingDistance)
        {
            task = Task.idle;
        }
    }
    protected virtual void Follwing()
    {
        if (targetToFollow)
        {
            nav.SetDestination(targetToFollow.position);
        }
        else
        {
            task = Task.idle;
        }
    }
    protected virtual void Chasing()
    {
        if (targetToFollow)
        {
            nav.SetDestination(targetToFollow.position);
            float distance = Vector3.Magnitude(nav.destination - transform.position);
            if (distance < attacDistance)
            {
                task = Task.attack;
            }
        }
        else
        {
            task = Task.idle;
        }
    }



    protected virtual void Animate()
    {
        var speedVector = nav.velocity;
        speedVector.y = 0;
        float speed = speedVector.magnitude;
        animator.SetFloat(ANIMATOR_SPEED, speed);
        animator.SetBool(ANIMATOR_ALIVE, IsAlive);
    }
    public virtual void Attack()
    {
        Unit unit = targetToFollow.GetComponent<Unit>();
        if (unit && unit.IsAlive)
        {
            animator.SetTrigger(ANIMATOR_ATTACK);
            attackTimer = attackCooldown;
        }
        else targetToFollow = null;

    }
    public virtual void DealDamage()
    {
        if(targetToFollow)
        {
            Unit unit = targetToFollow.GetComponent<Unit>();
            if (unit && unit.IsAlive)
            {
                unit.ReciveDamage(attackDamage);
            }

        }
    }

    public virtual void ReciveDamage(float Damage)
    {
        if(IsAlive) hp -= Damage;
        if(!IsAlive)
        {
            nav.enabled = false;
            healtBar.gameObject.SetActive(false);
            foreach(var collider in GetComponents<Collider>())
            {
                collider.enabled = false;
            }
            //enabled = false;
        }
    } 

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attacDistance);
    }

}
