using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class InfiniteTilemap : MonoBehaviour
{
    
    private Database database;
    private PlayerManager playerManager;
    private new Renderer renderer;

    private Vector2 startPosition;

    private float offset;

    private void Start()
    {
        database = GameManager.instance.database;
        startPosition = Vector2.zero;
        renderer = GetComponent<Renderer>();
        playerManager = PlayerManager.instance;
    }

    void Update()
    {
        if(playerManager.isDead) return;

        if (playerManager.hit)
        {
            playerManager.velocity = 0;
            return; 
        }
        playerManager.velocity = 1;
        renderer.material.SetTextureOffset("_MainTex", renderer.material.mainTextureOffset + playerManager.joystick.Direction * database.originVelocity * database.additionalVelocity * Time.deltaTime);
    }
}
