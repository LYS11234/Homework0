using Unity.VisualScripting;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Database Database;
    protected float damage;
    [SerializeField]
    protected float rotationSpeed;
    [SerializeField] 
    protected float additionalRotationSpeed;



    public virtual void SetVelocity()
    {
        GetComponentInParent<SpinningWeapons>().AdditionalRotateSpeed = additionalRotationSpeed;
        GetComponentInParent<SpinningWeapons>().RotateSpeed = rotationSpeed;
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
