using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Unit : MonoBehaviour
{
    public Transform targetToFollow;
    Animator animator;

    [SerializeField]
    float hp, hpMax = 100;
    [SerializeField]
    GameObject hpBarPrefab;
    public bool IsAlive { get { return hp > 0; } }
    public static List<ISeletable> SeletableUnit { get { return seletableUnit; } }

    [SerializeField]
    float stoppingDistance = 1;
    static List<ISeletable> seletableUnit = new List<ISeletable>();

    protected NavMeshAgent nav;
    protected HPBar healtBar;
    protected Task task = Task.idle;

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


    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        hp = hpMax;
        healtBar = Instantiate(hpBarPrefab, transform).GetComponent<HPBar>();
    }

    private void Start()
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

    protected virtual void Idling()
    {
        nav.velocity = Vector3.zero;
    }
    protected virtual void Attacking()
    {
        nav.velocity = Vector3.zero;
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
        //to do
    }



    protected virtual void Animate()
    {
        var speedVector = nav.velocity;
        speedVector.y = 0;
        float speed = speedVector.magnitude;
        animator.SetFloat(ANIMATOR_SPEED, speed);
        animator.SetBool(ANIMATOR_ALIVE, IsAlive);
    }
}
