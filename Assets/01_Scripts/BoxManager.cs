using UnityEngine;

public class BoxManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerManager playerManager;
    private Database database;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        database = Database.instance;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = -(playerManager.joystick.Direction * database.originVelocity * database.additionalVelocity * 20);
    }
}
