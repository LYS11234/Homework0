using UnityEngine;
using UnityEngine.UIElements;

public class BoxManager : MonoBehaviour, ISubject
{
    private Rigidbody2D rb;
    public PlayerManager playerManager;
    public Database database;
    private Animator animator;

    public IObserver Observer;


    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = -(playerManager.joystick.Direction * database.originVelocity * database.additionalVelocity * 20 * playerManager.velocity);
    }

    private void Update()
    {
        if(!animator.enabled)
        {
            return;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != 3)
        {
            return;
        }

        animator.enabled = true;
        
    }
    public void OpenBox()
    {
        NotifyObservers();
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
        Observer.ReleaseBox(gameObject);
    }
}
