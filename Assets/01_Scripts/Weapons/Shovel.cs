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
        //딜레이를 넣거나 큐 이용해서 무적시간 추가.
        base.OnTriggerEnter2D(collision);
    }
}
