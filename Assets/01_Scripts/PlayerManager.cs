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
    public FloatingJoystick Joystick;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Database Database;
    public SpinningWeapons Shovel;
    public SpinningWeapons Sickle; 


    #endregion

    #region Variables
    [Header("Variables")]
    public int Velocity;
    public float CurrentHp;

    public bool IsDead;
    #endregion

    public RaycastHit2D Hit;
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
        CurrentHp = Database.OriginHp;

        Shovel.Database = Database;
        Shovel.PlayerManager = this;
        Sickle.Database = Database;
        Sickle.PlayerManager = this;
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
        Hit = Physics2D.Raycast(transform.position, Joystick.Direction, 0.3f, layerMask);


        spriteRenderer.flipX = Joystick.Direction.x < 0;
        animator.SetBool("Move", Joystick.Direction != Vector2.zero);
    }


    public void Damage(float damage)
    {
        
        CurrentHp -= damage;
    }

    private void Die()
    {
        if (CurrentHp > 0)
        {
            return;
        }

        IsDead = true;
        animator.SetBool("Dead", IsDead);
        animator.Update(0); //바로 애니메이터에 변환값 적용

        NotifyObservers();
    }

}
