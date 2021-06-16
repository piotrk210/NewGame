using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Peasant : Unit, IInitializable
{

    [SerializeField] float chasingSpeed = 5;
    [SerializeField] float patrolRadious = 5;
    [SerializeField] float idlingCooldown = 2;

    const string ANIMATOR_ISONFARM = "Is On Farm";

    private IUnitHandler unitHandler;

    public Peasant(IUnitHandler _unitHandler)
    {
        unitHandler = _unitHandler;
    }

    //float normalSpeed;
    [SerializeField] GameObject textSpeakPrefab;
    bool IsOnFarm, IsInVillage;
    speakingText textSpeak;
    

    List<Dragon> seenMonster = new List<Dragon>();

    Vector3 startPoint;
    [SerializeField]GameObject village;
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
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestMonster = monster;
                }
            }
            return closestMonster;
        }
    }

    public void Initialize()
    {
        unitHandler.AddPeasant(this);
     
    }

    protected override void Start()
    {
        base.Start();
        textSpeak = Instantiate(textSpeakPrefab, transform).GetComponent<speakingText>();
        HideText();
    }


    protected override void Awake()
    {
        base.Awake();
        //normalSpeed = nav.speed;
        startPoint = transform.position;
        HideHpBar();
    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        var monster = other.gameObject.GetComponent<Dragon>();


        if(other.gameObject.name == "FarmCollider")
        {
            IsOnFarm = true;
            animator.SetBool(ANIMATOR_ISONFARM, IsOnFarm);
        }
        if (other.gameObject.name == "VillageCollider")
        {
            IsInVillage = true;
            IsOnFarm = false;
            animator.SetBool(ANIMATOR_ISONFARM, IsOnFarm);
        }
        if (monster && !seenMonster.Contains(monster))
        {
            CallText("Monsters!");
            healtBar.gameObject.SetActive(true);
            nav.SetDestination(village.transform.position);
            startPoint = village.transform.position;
            animator.SetBool(ANIMATOR_ISONFARM, IsOnFarm);
        }
        if(monster && !seenMonster.Contains(monster) && IsInVillage)
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
            healtBar.gameObject.SetActive(false);
        }
        if (other.gameObject.name == "VillageCollider")
        {
            IsInVillage = false;
        }
        if (other.gameObject.name == "FarmCollider")
        {

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
        if ((idlingTimer -= Time.deltaTime) <= 0)
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
        if (monster)
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

        nav.SetDestination(startPoint + delta);
    }



    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.blue;
        startPoint = transform.position;
        Gizmos.DrawWireSphere(startPoint, patrolRadious);
    }

    void CallText(string text)
    {
        textSpeak.gameObject.SetActive(true);
        textSpeak.SendMessage("ChangeTextValue", text);
        Invoke("HideText", 10);
    }

    void HideText ()
    {
        textSpeak.gameObject.SetActive(false);
    }

    void HideHpBar()
    {
        healtBar.gameObject.SetActive(false);
    }
}
