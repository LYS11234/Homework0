using Unity.VisualScripting;
using UnityEngine;

public class Shovel : Weapons
{
    public override void SetVelocity()
    {
        if (Database.IsUnityNull())
        {
            return;
        }
        rotationSpeed = Database.Shovel.OriginAngularVelocity;
        additionalRotationSpeed = Database.Shovel.AdditionalAngularVelocity;
        base.SetVelocity();
    }

    protected override void OnTriggerEnter2D(Collider2D _collision)
    {
        damage = Database.Shovel.Damage * Database.OriginAttack * Database.AdditionalAttack;
        //딜레이를 넣거나 큐 이용해서 무적시간 추가.
        base.OnTriggerEnter2D(_collision);
    }
}
