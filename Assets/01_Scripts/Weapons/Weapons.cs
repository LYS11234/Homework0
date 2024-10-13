using Unity.VisualScripting;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    protected Database database;
    protected float damage;
    protected float rotationSpeed;


    protected void Start()
    {
        database = GameManager.instance.database;
    }

    protected virtual void Update()
    {
        GetComponentInParent<SpinningWeapons>().additionalRotateSpeed = rotationSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        
        bool check = other.TryGetComponent<Enemy>(out Enemy enemy);
        if (!check)
        {
            return;
        }
        enemy.hp -= damage;
    }
}
