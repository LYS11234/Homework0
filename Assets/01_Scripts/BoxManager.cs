using UnityEngine;
using UnityEngine.UIElements;

public class BoxManager : MonoBehaviour, ISubject
{
    private Rigidbody2D rb;
    public PlayerManager PlayerManager;
    public Database Database;
    private Animator Animator;

    public IObserver Observer;


    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Animator = GetComponent<Animator>();
        Animator.enabled = false;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = -(PlayerManager.Joystick.Direction * Database.OriginVelocity * Database.AdditionalVelocity * 20 * PlayerManager.Velocity);
    }

    private void Update()
    {
        if(!Animator.enabled)
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

        Animator.enabled = true;
        
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
