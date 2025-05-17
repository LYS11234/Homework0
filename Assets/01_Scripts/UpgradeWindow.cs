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
    public PlayerManager PlayerManager;

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
        shovel = PlayerManager.Shovel;
        sickle = PlayerManager.Sickle;


        if (Database.AdditionalAttack >= 2)
        {
            upgrades[2].gameObject.SetActive(false);
        }
        if (Database.AdditionalVelocity >= 2)
        {
            upgrades[3].gameObject.SetActive(false);
        }
        if (Database.AdditionalHp >= 2)
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
        ++Database.Shovel.Level;
        switch (Database.Shovel.Level)
        {
            case 2:
                Database.Shovel.Damage += 0.4f;
                break;
            case 3:
                Database.Shovel.AdditionalAngularVelocity += 0.5f;
                break;
            case 4:
                GameObject go_shovel_1 = (GameObject)Instantiate(Resources.Load("Shovel"));
                go_shovel_1.transform.parent = shovel.transform;
                shovel.ChangeNumberOfWeapon();
                break;
            case 5:
                Database.Shovel.Damage += 0.4f;
                break;
            case 6:
                Database.Shovel.AdditionalAngularVelocity += 0.5f;
                break;
            case 7:
                GameObject go_shovel_2 = (GameObject)Instantiate(Resources.Load("Shovel"));
                go_shovel_2.transform.parent = shovel.transform;
                shovel.ChangeNumberOfWeapon();
                break;
            case 8:
                Database.Shovel.Damage += 0.4f;
                break;
            case 9:
                Database.Shovel.AdditionalAngularVelocity += 0.5f;
                break;
            case 10:
                GameObject go_shovel_3 = (GameObject)Instantiate(Resources.Load("Shovel"));
                go_shovel_3.transform.parent = shovel.transform;
                shovel.ChangeNumberOfWeapon();
                break;
            default:
                Database.Shovel.Damage += 0.05f;
                break;
        }
        NotifyObservers();

    }

    public void SickleUpgrade()
    {
        ++Database.Sickle.Level;
        switch (Database.Sickle.Level)
        {
            case 1:
                GameObject go_sickle_0 = (GameObject)Instantiate(Resources.Load("Shuriken"));
                go_sickle_0.transform.parent = sickle.transform;
                sickle.ChangeNumberOfWeapon();
                break;
            case 2:
                Database.Sickle.Damage += 0.4f;
                break;
            case 3:
                Database.Sickle.AdditionalAngularVelocity += 0.5f;
                break;
            case 4:
                GameObject go_sickle_1 = (GameObject)Instantiate(Resources.Load("Shuriken"));
                go_sickle_1.transform.parent = sickle.transform;
                sickle.ChangeNumberOfWeapon();
                break;
            case 5:
                Database.Sickle.Damage += 0.4f;
                break;
            case 6:
                Database.Sickle.AdditionalAngularVelocity += 0.5f;
                break;
            case 7:
                GameObject go_sickle_2 = (GameObject)Instantiate(Resources.Load("Shuriken"));
                go_sickle_2.transform.parent = sickle.transform;
                sickle.ChangeNumberOfWeapon();
                break;
            case 8:
                Database.Sickle.Damage += 0.4f;
                break;
            case 9:
                Database.Sickle.AdditionalAngularVelocity += 0.5f;
                break;
            case 10:
                GameObject go_sickle_3 = (GameObject)Instantiate(Resources.Load("Shuriken"));
                go_sickle_3.transform.parent = sickle.transform;
                sickle.ChangeNumberOfWeapon();
                break;
            default:
                Database.Sickle.Damage += 0.05f;
                break;
        }

        NotifyObservers();
    }

    public void AttackUpgrade()
    {
        Database.AdditionalAttack += 0.2f;
        NotifyObservers();
    }

    public void VelocityUpgrade()
    {
        Database.AdditionalVelocity += 0.2f;
        NotifyObservers();
    }

    public void HPUpgrade()
    {
        float nowMax = Database.OriginHp * Database.AdditionalHp;
        Database.AdditionalHp += 0.2f;
        PlayerManager.CurrentHp += (Database.OriginHp * Database.AdditionalHp - nowMax);
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
