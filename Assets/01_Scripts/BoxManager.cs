using UnityEngine;

public class BoxManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerManager playerManager;
    private Database database;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        database = GameManager.instance.database;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GetComponent<Animator>().enabled = false;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = -(playerManager.joystick.Direction * database.originVelocity * database.additionalVelocity * 20 * playerManager.velocity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != 3)
            return;

        GetComponent<Animator>().enabled = true;

        GameManager.instance.TurnOnItemReceiveUI();
        
        GameManager.instance.boxPool.Release(gameObject);
        
    }
}
