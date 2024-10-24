using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Unity.VisualScripting;

public class UpgradeWindow : MonoBehaviour, ISubject
{
    [SerializeField]
    private List<Button> upgrades;
    [SerializeField]
    public Database Database;
    private SpinningWeapons shovel;
    private SpinningWeapons sickle;
    public PlayerManager playerManager;

    private IObserver observer;

    private void Start()
    {
        
        //NotifyObservers();
    }


    private void OnEnable()
    {
        if (Database.IsUnityNull())
        {
            return;
        }
        shovel = playerManager.shovel;
        sickle = playerManager.sickle;


        if (Database.additionalAttack >= 2)
        {
            upgrades[2].gameObject.SetActive(false);
        }
        if (Database.additionalVelocity >= 2)
        {
            upgrades[3].gameObject.SetActive(false);
        }
        if (Database.additionalHp >= 2)
        {
            upgrades[4].gameObject.SetActive(false);
        }
    }

    void OnAssetLoaded(AsyncOperationHandle<Database> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Database = obj.Result; // 로드된 자산 인스턴스화
            
        }
        else
        {
            Debug.LogError("Failed to load Addressable Asset.");
        }
    }

    public void ShovelUpgrade()
    {
        ++Database.shovel.level;
        switch (Database.shovel.level)
        {
            case 2:
                Database.shovel.damage += 0.4f;
                break;
            case 3:
                Database.shovel.additionalAngularVelocity += 0.5f;
                break;
            case 4:
                GameObject go_shovel_1 = (GameObject)Instantiate(Resources.Load("Shovel"));
                go_shovel_1.transform.parent = shovel.transform;
                shovel.ChangeNumberOfWeapon();
                break;
            case 5:
                Database.shovel.damage += 0.4f;
                break;
            case 6:
                Database.shovel.additionalAngularVelocity += 0.5f;
                break;
            case 7:
                GameObject go_shovel_2 = (GameObject)Instantiate(Resources.Load("Shovel"));
                go_shovel_2.transform.parent = shovel.transform;
                shovel.ChangeNumberOfWeapon();
                break;
            case 8:
                Database.shovel.damage += 0.4f;
                break;
            case 9:
                Database.shovel.additionalAngularVelocity += 0.5f;
                break;
            case 10:
                GameObject go_shovel_3 = (GameObject)Instantiate(Resources.Load("Shovel"));
                go_shovel_3.transform.parent = shovel.transform;
                shovel.ChangeNumberOfWeapon();
                break;
            default:
                Database.shovel.damage += 0.05f;
                break;
        }
        NotifyObservers();

    }

    public void SickleUpgrade()
    {
        ++Database.sickle.level;
        switch (Database.sickle.level)
        {
            case 1:
                GameObject go_sickle_0 = (GameObject)Instantiate(Resources.Load("Shuriken"));
                go_sickle_0.transform.parent = sickle.transform;
                sickle.ChangeNumberOfWeapon();
                break;
            case 2:
                Database.sickle.damage += 0.4f;
                break;
            case 3:
                Database.sickle.additionalAngularVelocity += 0.5f;
                break;
            case 4:
                GameObject go_sickle_1 = (GameObject)Instantiate(Resources.Load("Shuriken"));
                go_sickle_1.transform.parent = sickle.transform;
                sickle.ChangeNumberOfWeapon();
                break;
            case 5:
                Database.sickle.damage += 0.4f;
                break;
            case 6:
                Database.sickle.additionalAngularVelocity += 0.5f;
                break;
            case 7:
                GameObject go_sickle_2 = (GameObject)Instantiate(Resources.Load("Shuriken"));
                go_sickle_2.transform.parent = sickle.transform;
                sickle.ChangeNumberOfWeapon();
                break;
            case 8:
                Database.sickle.damage += 0.4f;
                break;
            case 9:
                Database.sickle.additionalAngularVelocity += 0.5f;
                break;
            case 10:
                GameObject go_sickle_3 = (GameObject)Instantiate(Resources.Load("Shuriken"));
                go_sickle_3.transform.parent = sickle.transform;
                sickle.ChangeNumberOfWeapon();
                break;
            default:
                Database.sickle.damage += 0.05f;
                break;
        }

        NotifyObservers();
    }

    public void AttackUpgrade()
    {
        Database.additionalAttack += 0.2f;
        NotifyObservers();
    }

    public void VelocityUpgrade()
    {
        Database.additionalVelocity += 0.2f;
        NotifyObservers();
    }

    public void HPUpgrade()
    {
        float nowMax = Database.originHp * Database.additionalHp;
        Database.additionalHp += 0.2f;
        playerManager.currentHp += (Database.originHp * Database.additionalHp - nowMax);
        NotifyObservers();
    }

    public void RegisterObserver(IObserver ob)
    {
        observer = ob;
    }

    public void RemoveObserver(IObserver ob)
    {
        observer = null;
    }

    public void NotifyObservers()
    {
        observer.TurnOffBackground();
    }
}
