using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Unit : MonoBehaviour
{
    NavMeshAgent nav;
    public Transform targetToFollow;
    Animator animator;

    [SerializeField]
    float hp, hpMax = 100;

    [SerializeField]
    GameObject hpBarPrefab;

    public static List<ISeletable> SeletableUnit { get { return seletableUnit; } }
    static List<ISeletable> seletableUnit = new List<ISeletable>();

    protected HPBar healtBar;

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
        healtBar =  Instantiate(hpBarPrefab, transform).GetComponent<HPBar>();
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
        if(targetToFollow)
        {
            nav.SetDestination(targetToFollow.position);
        }
        Animate();

    }
    protected virtual void Animate()
    {
        var speedVector = nav.velocity;
        speedVector.y = 0;
        float speed = speedVector.magnitude;
        animator.SetFloat(ANIMATOR_SPEED, speed);
        animator.SetBool(ANIMATOR_ALIVE, hp>0);
    }
}
