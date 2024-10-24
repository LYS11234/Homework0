using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerManager : MonoBehaviour, ISubject
{

    #region Components
    [Header("Components")]
    public FloatingJoystick joystick;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Database database;
    public SpinningWeapons shovel;
    public SpinningWeapons sickle; 


    #endregion

    #region Variables
    [Header("Variables")]
    public int velocity;
    public float currentHp;

    public bool isDead;
    #endregion

    public RaycastHit2D hit;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    public IObserver Observer;


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
        Observer.PlayerDead();
    }

    private void Start()
    {
        
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHp = database.originHp;

        shovel.database = database;
        shovel.playerManager = this;
        sickle.database = database;
        sickle.playerManager = this;
    }
    //void OnAssetLoaded(AsyncOperationHandle<Database> obj)
    //{
    //    if (obj.Status == AsyncOperationStatus.Succeeded)
    //    {
    //        database = obj.Result; // 로드된 자산 인스턴스화
            
    //    }
    //    else
    //    {
    //        Debug.LogError("Failed to load Addressable Asset.");
    //    }
    //}
    private void Update()
    {
        if(spriteRenderer.IsUnityNull())
        {
            return;
        }
        Die();
    }

    private void FixedUpdate()
    {
        if (animator.IsUnityNull())
        {
            return;
        }
        if (animator.GetBool("Dead"))
        {
            return;
        }

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
        {
            return;
        }

        isDead = true;
        animator.SetBool("Dead", isDead);
        animator.Update(0); //바로 애니메이터에 변환값 적용

        NotifyObservers();
    }

}
