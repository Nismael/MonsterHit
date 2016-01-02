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

    private Animator _animator;
    private eGremlinState mState;
    private int animTime = 90;

    enum eGremlinState
    {
        Idle,
        Walking,
        Attacking,
        Dying
    }

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        mState = eGremlinState.Idle;
    }

    public void Walk()
    {
        mState = eGremlinState.Walking;
        _animator.SetTrigger("startWalking");
    }

    public void Attack()
    {
        mState = eGremlinState.Attacking;
        _animator.SetTrigger("startAttacking");
        OnMonsterAttack(this);
    }

    public void Kill()
    {
        //mState = eGremlinState.;
    }

    void Update()
    {
        switch (mState)
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
                animTime--;
                if (!_animator.IsInTransition(0) && animTime <= 0)
                {
                    OnActionsCompleted(this);
                }
                break;
            }
        }
    }

    void OnMouseDown()
    {
        mState = eGremlinState.Dying;
        _animator.SetTrigger("startDying");
        OnMonsterKilled(this);
    }

    void OnGUI()
    {
        if (mState == eGremlinState.Dying)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position); 
            GUI.Label(new Rect(screenPoint.x-5, Screen.height - screenPoint.y - 35, 100, 50), "50");
        }                
    }
}

