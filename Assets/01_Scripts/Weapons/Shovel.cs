using Unity.VisualScripting;
using UnityEngine;

public class Shovel : Weapons
{

    protected override void Update()
    {
        rotationSpeed = database.shovel.additionalAngularVelocity;
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        damage = database.shovel.damage * database.originAttack * database.additionalAttack;
        base.OnTriggerEnter2D(collision);
    }
}
