using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using Unity.VisualScripting;

public class InfiniteTilemap : MonoBehaviour
{
    
    public Database database;
    [SerializeField]
    private PlayerManager playerManager;
    private new Renderer renderer;

    private Vector2 startPosition;

    private float offset;

    private void Start()
    {

        startPosition = Vector2.zero;
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if(database.IsUnityNull())
        {
            return;
        }

        if (playerManager.isDead)
        {
            return;
        }

        if (playerManager.hit)
        {
            playerManager.velocity = 0;
            return; 
        }

        playerManager.velocity = 1;
        renderer.material.SetTextureOffset("_MainTex", renderer.material.mainTextureOffset + playerManager.joystick.Direction * database.originVelocity * database.additionalVelocity * Time.deltaTime);
    }
}
