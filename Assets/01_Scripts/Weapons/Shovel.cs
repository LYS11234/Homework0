using Unity.VisualScripting;
using UnityEngine;

public class Shovel : Weapons
{
    protected override void Update()
    {
        rotationSpeed = Database.Shovel.AdditionalAngularVelocity;
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D _collision)
    {
        damage = Database.Shovel.Damage * Database.OriginAttack * Database.AdditionalAttack;
        //딜레이를 넣거나 큐 이용해서 무적시간 추가.
        base.OnTriggerEnter2D(_collision);
    }
}
