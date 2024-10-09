using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    #region Components
    [Header("Components")]
    public FloatingJoystick joystick;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Database database;
    #endregion

    #region Variables
    [Header("Variables")]
    public int velocity;
    [SerializeField]
    private float currentHp;
    public float maxHp;
    #endregion

    public RaycastHit2D hit;
    [SerializeField]
    private LayerMask layerMask;

    private void Start()
    {
        database = Database.instance;
        maxHp = database.originHp;
        currentHp = maxHp;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Die();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        
    }

    private void Move()
    {
        //rigidBody.linearVelocity = joystick.Direction * velocity;
        hit = Physics2D.Raycast(transform.position, joystick.Direction, 0.3f, layerMask);


        spriteRenderer.flipX = joystick.Direction.x < 0;
        animator.SetBool("Move", joystick.Direction != Vector2.zero);
    }

    public void Damage(float damage)
    {
        
        currentHp -= damage;
    }

    private void Die()
    {
        if (currentHp > 0)
            return;

        animator.SetBool("Dead", true);

        
    }

}
