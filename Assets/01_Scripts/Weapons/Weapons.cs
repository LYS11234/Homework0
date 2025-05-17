using Unity.VisualScripting;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Database Database;
    protected float damage;
    protected float rotationSpeed;


    protected void Start()
    {
    }

    protected virtual void Update()
    {
        GetComponentInParent<SpinningWeapons>().AdditionalRotateSpeed = rotationSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        
        bool check = other.TryGetComponent<Enemy>(out Enemy enemy);
        if (!check)
        {
            return;
        }
        enemy.HP -= damage;
    }
}
