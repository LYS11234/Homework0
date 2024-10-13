using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float maxHp;
    private float deleteTime;
    public float velocity;
    private float attackterm;
    private float attack;
    private bool isOn;

    private PlayerManager playerManager;
    private Database database;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rig;
    private BoxCollider2D collider;

    private Vector2 direction;
    private RaycastHit2D hit;
    [SerializeField]
    private LayerMask layerMask;
    void Start()
    {
        attack = 2;
        collider = GetComponent<BoxCollider2D>();
        playerManager = PlayerManager.instance;
        database = GameManager.instance.database;
        rig= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Spawn();
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
        collider.enabled = true;
        float x = UnityEngine.Random.Range(-10, 10.0f);
        float y = Mathf.Pow(100 - Mathf.Pow(x, 2), 0.5f);
        int minus = UnityEngine.Random.Range(0, 2);
        y *= Mathf.Pow(-1, minus);
        transform.localPosition = new Vector2(x, y);
        hp = maxHp * (1 + database.stage * 0.1f);
    }
    private void FixedUpdate()
    {
        Move();
        Die();
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
            spriteRenderer.flipX = (transform.position.x > playerManager.transform.position.x);
    }

    public void Damage(float damage)
    {
        hp -= damage;
        animator.SetTrigger("Hit");
    }

    private void Die()
    {
        if (hp > 0)
            return;
        collider.enabled = false;
        if (!animator.GetBool("Dead"))
        {
            animator.SetBool("Dead", true);
            GameManager.instance.currentSpawnCount--;
        }
        

        gameObject.layer = 0;
        
        if (deleteTime < 3)
            deleteTime += Time.deltaTime;
        else
        {
            deleteTime = 0;
            GameManager.instance.BoxDrop(transform.position);
            GameManager.instance.spawnPool.Release(gameObject);
        }
        rig.linearVelocity = -(playerManager.joystick.Direction * database.originVelocity * database.additionalVelocity * 20 * playerManager.velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        bool canAttack = collision.TryGetComponent<PlayerManager>(out PlayerManager _playerManager);
        if (!canAttack)
            return;

        playerManager.Damage(attack);
        isOn = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        bool canAttack = collision.TryGetComponent<PlayerManager>(out PlayerManager _playerManager);
        if (!canAttack)
            return;
        isOn = false;
    }

}
