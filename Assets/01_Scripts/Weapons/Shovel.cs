using Unity.VisualScripting;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    private Database database;

    private void Start()
    {
        database = Database.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool check = collision.TryGetComponent<EnemyManager>(out EnemyManager enemy);
        if (!check)
        {
            return;
        }
        enemy.hp -= database.shovel.damage * database.attack;
    }
}
