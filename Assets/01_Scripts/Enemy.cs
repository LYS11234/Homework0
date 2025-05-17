using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Enemy : MonoBehaviour, ISubject
{
    public float HP;
    public float MaxHp;
    [SerializeField]
    private float deleteTime;
    public float Velocity;
    [SerializeField]
    private float attackterm;
    private float attack;
    [SerializeField]
    private bool isOn;
    [SerializeField]
    private bool isDead;


    public PlayerManager PlayerManager;
    public Database Database;
    [SerializeField]
    private Animator Animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Rigidbody2D rig;
    [SerializeField]
    private BoxCollider2D collider;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private RaycastHit2D hit;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private IObserver observer;

    void Start()
    {
        attack = 2;
        collider = GetComponent<BoxCollider2D>();
        Spawn();
        rig = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnEnable()
    {
        if(Database.IsUnityNull())
            return;
        
        Spawn();
    }

    private void Spawn()
    {
        gameObject.layer = 7;
        collider.enabled = true;
        float x = UnityEngine.Random.Range(-10, 10.0f);
        float y = Mathf.Pow(100 - Mathf.Pow(x, 2), 0.5f);
        int minus = UnityEngine.Random.Range(0, 2);
        y *= Mathf.Pow(-1, minus);
        transform.localPosition = new Vector2(x, y);
        HP = MaxHp * (1 + Database.Stage * 0.1f);
        isDead = false;
    }
    private void FixedUpdate()
    {
        if(collider.IsUnityNull())
        {
            return;
        }
        Die();
        if (Animator.GetBool("Dead"))
        {
            
            return;
        }
        Move();
        
        direction = (PlayerManager.transform.position - transform.position).normalized;
    }


    private void Update()
    {
        if (!isOn)
            return;
        if (attackterm >= 1f)
        {
            PlayerManager.Damage(attack);
            attackterm = 0;
        }
        else
            attackterm += Time.deltaTime;

    }

    public void RegisterObserver(IObserver _observer)
    {
        observer = _observer;
    }

    public void RemoveObserver(IObserver _observer)
    {
        observer = null;
    }

    public void NotifyObservers()
    {
        if (!isDead)
        {
            observer.EnemyDead(transform);
            isDead = true;
        }
        if (deleteTime < 3)
            deleteTime += Time.deltaTime;
        else
        {
            deleteTime = 0;
            observer.EnemyRelease(gameObject);
        }
        
    }

    private void Move()
    {
        hit = Physics2D.Raycast(transform.position, direction.normalized, 0.3f, layerMask);

        
        
        if (hit)
        {
            rig.linearVelocity = -(PlayerManager.Joystick.Direction * Database.OriginVelocity * Database.AdditionalVelocity * 20 * PlayerManager.Velocity);
            Animator.SetBool("Move", false);
            
        }
        else
        {
            rig.linearVelocity = -(PlayerManager.Joystick.Direction * Database.OriginVelocity * Database.AdditionalVelocity * 20 * PlayerManager.Velocity) + direction * Velocity;
            Animator.SetBool("Move", true);
        }
        if(HP > 0)
            spriteRenderer.flipX = (transform.position.x > 0);
    }

    public void Damage(float _damage)
    {
        HP -= _damage;
        Animator.SetTrigger("Hit");
    }

    private void Die()
    {
        if (HP > 0)
        {
            return;
        }
        collider.enabled = false;
        if (!Animator.GetBool("Dead"))
        {
            Animator.SetBool("Dead", true);
            Animator.Update(0);
            
        }
        rig.linearVelocity = -(PlayerManager.Joystick.Direction * Database.OriginVelocity * Database.AdditionalVelocity * 20 * PlayerManager.Velocity);

        gameObject.layer = 0;
        NotifyObservers();

        
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {

        bool canAttack = _collision.TryGetComponent<PlayerManager>(out PlayerManager _playerManager);
        if (!canAttack)
        {
            return;
        }
        _playerManager.Damage(attack);
        isOn = true;
    }


    private void OnTriggerExit2D(Collider2D _collision)
    {
        bool canAttack = _collision.TryGetComponent<PlayerManager>(out PlayerManager _playerManager);
        if (!canAttack)
        {
            return;
        }

        isOn = false;
    }

}
