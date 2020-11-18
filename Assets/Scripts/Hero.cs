using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{

    [SerializeField] float chasingSpeed = 5;
    [SerializeField] float patrolRadious = 5;
    [SerializeField] float idlingCooldown = 2;

    float normalSpeed;

    List<Dragon> seenMonster = new List<Dragon>();

    Vector3 startPoint;
    float idlingTimer;
    Dragon ClosestMonster
    {
        get
        {
            if (seenMonster == null || seenMonster.Count <= 0) return null;
            float minDistance = float.MaxValue;
            Dragon closestMonster = null;
            foreach (Dragon monster in seenMonster)
            {
                if (!monster || !monster.IsAlive) continue;
                float distance = Vector3.Magnitude(monster.transform.position - transform.position);
                if(distance<minDistance)
                {
                    minDistance = distance;
                    closestMonster = monster;
                }
            }
            return closestMonster;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        normalSpeed = nav.speed;
        startPoint = transform.position;
    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        var monster = other.gameObject.GetComponent<Dragon>();
        if (monster && !seenMonster.Contains(monster))
        {
            seenMonster.Add(monster);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        var monster = other.gameObject.GetComponent<Dragon>();
        if (monster)
        {
            seenMonster.Remove(monster);
        }
    }

    protected override void Moving()
    {
        base.Moving();
        UpdateSight();
    }

    protected override void Idling()
    {
        base.Idling();
        UpdateSight();
        if((idlingTimer -=Time.deltaTime)<=0)
        {
            idlingTimer = idlingCooldown;
            task = Task.move;
            SetRandomRoamingPosition();
        }
    }

    protected override void Chasing()
    {
        base.Chasing();
        nav.speed = chasingSpeed;
    }

    void UpdateSight()
    {
        var monster = ClosestMonster;
        if(monster)
        {
            targetToFollow = monster.transform;
            task = Task.chase;
        }
    }

    void SetRandomRoamingPosition()
    {
     
        Vector3 delta = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        delta.Normalize();
        delta *= patrolRadious;

        nav.SetDestination(startPoint+delta);
    }



    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.blue;
        startPoint = transform.position;
        Gizmos.DrawWireSphere(startPoint, patrolRadious);
    }
}
