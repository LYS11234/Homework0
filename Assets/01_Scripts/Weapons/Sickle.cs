using UnityEngine;

public class Sickle : Weapons
{
    
    protected override void Update()
    {
        rotationSpeed = database.sickle.additionalAngularVelocity;
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        damage = database.originAttack * database.additionalAttack * database.sickle.damage;
        base.OnTriggerEnter2D(other);
    }
}
