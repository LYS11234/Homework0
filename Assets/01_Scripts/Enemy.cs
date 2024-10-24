using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Enemy : MonoBehaviour, ISubject
{
    public float hp;
    public float maxHp;
    private float deleteTime;
    public float velocity;
    private float attackterm;
    private float attack;
    private bool isOn;
    private bool isDead;


    public PlayerManager playerManager;
    public Database database;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rig;
    private BoxCollider2D _collider;

    private Vector2 direction;
    private RaycastHit2D hit;
    [SerializeField]
    private LayerMask layerMask;

    private IObserver Observer;

    void Start()
    {
        attack = 2;
        _collider = GetComponent<BoxCollider2D>();
        Spawn();
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnEnable()
    {
        if(database.IsUnityNull())
            return;
        
        Spawn();
    }

    private void Spawn()
    {
        gameObject.layer = 7;
        _collider.enabled = true;
        float x = UnityEngine.Random.Range(-10, 10.0f);
        float y = Mathf.Pow(100 - Mathf.Pow(x, 2), 0.5f);
        int minus = UnityEngine.Random.Range(0, 2);
        y *= Mathf.Pow(-1, minus);
        transform.localPosition = new Vector2(x, y);
        hp = maxHp * (1 + database.stage * 0.1f);
        isDead = false;
    }
    private void FixedUpdate()
    {
        if(_collider.IsUnityNull())
        {
            return;
        }
        Die();
        if (animator.GetBool("Dead"))
        {
            
            return;
        }
        Move();
        
        direction = (playerManager.transform.position - transform.position).normalized;
    }


    private void Update()
    {
        if (!isOn)
            return;
        if (attackterm >= 1f)
        {
            playerManager.Damage(attack);
            attackterm = 0;
        }
        else
            attackterm += Time.deltaTime;

    }

    public void RegisterObserver(IObserver observer)
    {
        Observer = observer;
    }

    public void RemoveObserver(IObserver observer)
    {
        Observer = null;
    }

    public void NotifyObservers()
    {
        if (!isDead)
        {
            Observer.EnemyDead(transform);
            isDead = true;
        }
        if (deleteTime < 3)
            deleteTime += Time.deltaTime;
        else
        {
            deleteTime = 0;
            Observer.EnemyRelease(gameObject);
        }
        
    }

    private void Move()
    {
        hit = Physics2D.Raycast(transform.position, direction.normalized, 0.3f, layerMask);

        
        
        if (hit)
        {
            rig.linearVelocity = -(playerManager.joystick.Direction * database.originVelocity * database.additionalVelocity * 20 * playerManager.velocity);
            animator.SetBool("Move", false);
            
        }
        else
        {
            rig.linearVelocity = -(playerManager.joystick.Direction * database.originVelocity * database.additionalVelocity * 20 * playerManager.velocity) + direction * velocity;
            animator.SetBool("Move", true);
        }
        if(hp > 0)
            spriteRenderer.flipX = (transform.position.x > 0);
    }

    public void Damage(float damage)
    {
        hp -= damage;
        animator.SetTrigger("Hit");
    }

    private void Die()
    {
        if (hp > 0)
        {
            return;
        }
        _collider.enabled = false;
        if (!animator.GetBool("Dead"))
        {
            animator.SetBool("Dead", true);
            animator.Update(0);
            
        }
        rig.linearVelocity = -(playerManager.joystick.Direction * database.originVelocity * database.additionalVelocity * 20 * playerManager.velocity);

        gameObject.layer = 0;
        NotifyObservers();

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        bool canAttack = collision.TryGetComponent<PlayerManager>(out PlayerManager _playerManager);
        if (!canAttack)
        {
            return;
        }
        _playerManager.Damage(attack);
        isOn = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        bool canAttack = collision.TryGetComponent<PlayerManager>(out PlayerManager _playerManager);
        if (!canAttack)
        {
            return;
        }

        isOn = false;
    }

}
