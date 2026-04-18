using UnityEngine;

public class Sickle : Weapons
{
    
    public override void SetVelocity()
    {
        rotationSpeed = Database.Sickle.OriginAngularVelocity;
        additionalRotationSpeed = Database.Sickle.AdditionalAngularVelocity;
        base.SetVelocity();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        damage = Database.OriginAttack * Database.AdditionalAttack * Database.Sickle.Damage;
        base.OnTriggerEnter2D(other);
    }
}
