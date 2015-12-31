using UnityEngine;
using System.Collections;

public class Gremlin : MonoBehaviour
{
    public delegate void AttackCompleted(GameObject obj);
    public event AttackCompleted OnAttackCompleted;

    public float Speed { get; set; }
    public float Damage { get; set; }

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
                    OnAttackCompleted(this.gameObject);
                }
                break;
            }
        }
    }

    void OnMouseDown()
    {
        mState = eGremlinState.Dying;
        _animator.SetTrigger("startDying");
    }
}

