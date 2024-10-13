using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : MonoBehaviour
{
    [SerializeField]
    private List<Button> upgrades;
    private Database database;
    private SpinningWeapons shovel;
    private SpinningWeapons sickle;
    GameManager gameManager;
    private PlayerManager playerManager;

    private void Start()
    {
        
    }


    private void OnEnable()
    {
        playerManager = PlayerManager.instance;
        gameManager = GameManager.instance;
        database = gameManager.database;
        shovel = playerManager.shovel;
        sickle = playerManager.sickle;
        if (database.shovel.level >= 10)
            upgrades[0].gameObject.SetActive(false);
        if (database.sickle.level >= 10)
            upgrades[1].gameObject.SetActive(false);
        if (database.additionalAttack >= 2)
            upgrades[2].gameObject.SetActive(false);
        if (database.additionalVelocity >= 2)
            upgrades[3].gameObject.SetActive(false);
        if (database.additionalHp >= 2)
            upgrades[4].gameObject.SetActive(false);
    }

    public void ShovelUpgrade()
    {
        ++database.shovel.level;
        switch (database.shovel.level)
        {
            case 2:
                database.shovel.damage += 0.4f;
                break;
            case 3:
                database.shovel.additionalAngularVelocity += 0.5f;
                break;
            case 4:
                GameObject go_shovel_1 = (GameObject)Instantiate(Resources.Load("Shovel"));
                go_shovel_1.transform.parent = shovel.transform;
                shovel.ChangeNumberOfWeapon();
                break;
            case 5:
                database.shovel.damage += 0.4f;
                break;
            case 6:
                database.shovel.additionalAngularVelocity += 0.5f;
                break;
            case 7:
                GameObject go_shovel_2 = (GameObject)Instantiate(Resources.Load("Shovel"));
                go_shovel_2.transform.parent = shovel.transform;
                shovel.ChangeNumberOfWeapon();
                break;
            case 8:
                database.shovel.damage += 0.4f;
                break;
            case 9:
                database.shovel.additionalAngularVelocity += 0.5f;
                break;
            case 10:
                GameObject go_shovel_3 = (GameObject)Instantiate(Resources.Load("Shovel"));
                go_shovel_3.transform.parent = shovel.transform;
                shovel.ChangeNumberOfWeapon();
                break;
            default:
                Debug.Log("Error!");
                break;
        }
        gameManager.TurnOffBackground();
        
    }

    public void SickleUpgrade()
    {
        ++database.sickle.level;
        switch (database.sickle.level)
        {
            case 1:
                GameObject go_sickle_0 = (GameObject)Instantiate(Resources.Load("Sickle"));
                go_sickle_0.transform.parent = sickle.transform;
                sickle.ChangeNumberOfWeapon();
                break;
            case 2:
                database.sickle.damage += 0.4f;
                break;
            case 3:
                database.sickle.additionalAngularVelocity += 0.5f;
                break;
            case 4:
                GameObject go_sickle_1 = (GameObject)Instantiate(Resources.Load("Sickle"));
                go_sickle_1.transform.parent = sickle.transform;
                sickle.ChangeNumberOfWeapon();
                break;
            case 5:
                database.sickle.damage += 0.4f;
                break;
            case 6:
                database.sickle.additionalAngularVelocity += 0.5f;
                break;
            case 7:
                GameObject go_sickle_2 = (GameObject)Instantiate(Resources.Load("Sickle"));
                go_sickle_2.transform.parent = sickle.transform;
                sickle.ChangeNumberOfWeapon();
                break;
            case 8:
                database.sickle.damage += 0.4f;
                break;
            case 9:
                database.sickle.additionalAngularVelocity += 0.5f;
                break;
            case 10:
                GameObject go_sickle_3 = (GameObject)Instantiate(Resources.Load("Sickle"));
                go_sickle_3.transform.parent = sickle.transform;
                sickle.ChangeNumberOfWeapon();
                break;
            default:
                Debug.Log("Error!");
                break;
        }

        gameManager.TurnOffBackground();
    }

    public void AttackUpgrade()
    {
        database.additionalAttack += 0.2f;
        gameManager.TurnOffBackground();
    }

    public void VelocityUpgrade()
    {
        database.additionalVelocity += 0.2f;
        gameManager.TurnOffBackground();
    }

    public void HPUpgrade()
    {
        float nowMax = database.originHp * database.additionalHp;
        database.additionalHp += 0.2f;
        playerManager.currentHp += (database.originHp * database.additionalHp - nowMax);
    }
}
