using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using Unity.VisualScripting;

public class InfiniteTilemap : MonoBehaviour, ISubject
{

    public Database Database;
    [SerializeField]
    private PlayerManager playerManager;
    private new Renderer renderer;

    private Vector2 startPosition;
    private IObserver observer;

    private float offset;

    private void Start()
    {

        startPosition = Vector2.zero;
        renderer = GetComponent<Renderer>();

        RegisterObserver(playerManager.Observer);
    }

    void Update()
    {
        if (Database.IsUnityNull())
        {
            return;
        }

        if (playerManager.IsDead)
        {
            return;
        }

        if (playerManager.Hit)
        {
            playerManager.Velocity = 0;
            return;
        }

        playerManager.Velocity = 1;
        renderer.material.SetTextureOffset("_MainTex", renderer.material.mainTextureOffset + playerManager.Joystick.Direction * Database.OriginVelocity * Database.AdditionalVelocity * Time.deltaTime);
        NotifyObservers();
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
        observer.MovePosition(playerManager.Joystick.Direction * Database.OriginVelocity * Database.AdditionalVelocity * Time.deltaTime);
    }
}
