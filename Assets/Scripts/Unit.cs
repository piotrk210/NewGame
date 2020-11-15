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
        Instantiate(hpBarPrefab, transform);
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
