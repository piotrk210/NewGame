using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Unit, ISeletable
{

    [Header("Dragon")]

    [Range(0, .3f), SerializeField] float attackDuration = 0;
    [SerializeField] LayerMask bitingLayerMask;

    protected override void Awake()
    {
        base.Awake();
        GameController.DragonList.Add(this);
    }

    public void SetSelected(bool selected)
    {
        healtBar.gameObject.SetActive(selected);
    }

    void Command ( Vector3 destination)
    {
        nav.SetDestination(destination);
        task = Task.move;
        targetToFollow = null;
    }
    void Command(Dragon dragonToFollow)
    {
        targetToFollow = dragonToFollow.transform;
        task = Task.follow;
    }
    void Command(Hero heroToKill)
    {
        targetToFollow = heroToKill.transform;
        task = Task.chase;
        Debug.Log(heroToKill.gameObject);
    }

    void Command(Peasant peasantToKill)
    {
        targetToFollow = peasantToKill.transform;
        task = Task.chase;
        Debug.Log(peasantToKill.gameObject);
    }

    public override void DealDamage()
    {
        if(Bite())
        base.DealDamage();

    }

    bool Bite()
    {
        Vector3 start = transform.position;
        Vector3 direction = transform.forward;
        RaycastHit hit;
        if(Physics.Raycast(start, direction, out hit,attacDistance, bitingLayerMask))
        {
            var unit = hit.collider.gameObject.GetComponent<Unit>();
            return unit;
        }
        return false;
    }
}
