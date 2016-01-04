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

    private AudioClip sfxWalk;
    private AudioClip sfxAttack;
    private AudioClip sfxDie;

    private AudioSource _AudioSource;
    public AudioClip SpawnClip;
    public AudioClip KillClip;
    public AudioClip AttackClip;

    enum eGremlinState
    {
        Idle,
        Walking,
        Attacking,
        Dying
    }

    /*AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip; 
        newAudio.loop = loop; 
        newAudio.playOnAwake = playAwake; 
        newAudio.volume = vol;
        return newAudio;
    }*/

    public void Awake()
    {
        _Animator = GetComponent<Animator>();
        _AudioSource = GetComponent<AudioSource>();
        State = eGremlinState.Idle;
    }

    public void Start()
    {
        _AudioSource.PlayOneShot(SpawnClip, 1.0f);
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
        _AudioSource.PlayOneShot(AttackClip);
    }

    public void Kill()
    {
        State = eGremlinState.Dying;
        _Animator.SetTrigger("startDying");
        OnMonsterKilled(this);
        _AudioSource.PlayOneShot(KillClip, 1.0f);
    }

    public void Stop()
    {
        if (State != eGremlinState.Dying)
        {
            State = eGremlinState.Idle;
            _Animator.Play("Idle");
        }
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
        Kill();
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

