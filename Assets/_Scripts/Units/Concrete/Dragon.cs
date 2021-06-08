using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Dragon : Unit, ISeletable, IInitializable 
{

    [Header("Dragon")]

    [Range(0, .3f), SerializeField] float attackDuration = 0;
    [SerializeField] LayerMask bitingLayerMask;

    private IGameController gameController;

    public Dragon(IGameController _gameController)
    {
        gameController = _gameController;
    }

    public void Initialize()
    {
        gameController.AddDragon(this);
        
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
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
        //Debug.Log(heroToKill.gameObject);
    }

    void Command(Peasant peasantToKill)
    {
        targetToFollow = peasantToKill.transform;
        task = Task.chase;
        //Debug.Log(peasantToKill.gameObject);
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
