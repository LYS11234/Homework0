using UnityEngine;

public class Sickle : Weapons
{
    
    protected override void Update()
    {
        rotationSpeed = Database.Sickle.AdditionalAngularVelocity;
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        damage = Database.OriginAttack * Database.AdditionalAttack * Database.Sickle.Damage;
        base.OnTriggerEnter2D(other);
    }
}
