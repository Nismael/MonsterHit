using UnityEngine;
using System.Collections;

public class Gremlin : MonoBehaviour
{
    public delegate void GremlinAction(Gremlin obj);
    public event GremlinAction OnActionsCompleted;
    public event GremlinAction OnMonsterKilled;
    public event GremlinAction OnMonsterAttack;

    public float Speed { get; set; }
    public int Damage { get; set; }

    private Animator _Animator;
    private eGremlinState State;
    private int AnimTime = 90;

    enum eGremlinState
    {
        Idle,
        Walking,
        Attacking,
        Dying
    }

    public void Awake()
    {
        _Animator = GetComponent<Animator>();
        State = eGremlinState.Idle;
    }

    public void Walk()
    {
        State = eGremlinState.Walking;
        _Animator.SetTrigger("startWalking");
    }

    public void Attack()
    {
        State = eGremlinState.Attacking;
        _Animator.SetTrigger("startAttacking");
        OnMonsterAttack(this);
    }

    public void Kill()
    {
        //mState = eGremlinState.;
    }

    void Update()
    {
        switch (State)
        {
            case eGremlinState.Walking:
            {
                transform.Translate(0, -Speed * Time.deltaTime, 0);
                break;
            }
            case eGremlinState.Attacking:
            case eGremlinState.Dying:
            {
                //I wish there was a better mechanism to hook callbacks to an animation timeline. Just hacking for now
                AnimTime--;
                if (!_Animator.IsInTransition(0) && AnimTime <= 0)
                {
                    OnActionsCompleted(this);
                }
                break;
            }
        }
    }

    void OnMouseDown()
    {
        State = eGremlinState.Dying;
        _Animator.SetTrigger("startDying");
        OnMonsterKilled(this);
    }

    void OnGUI()
    {
        if (State == eGremlinState.Dying)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position); 
            GUI.Label(new Rect(screenPoint.x-5, Screen.height - screenPoint.y - 35, 100, 50), "50");
        }                
    }
}

