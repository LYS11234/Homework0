using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections.Generic;

using Random = UnityEngine.Random;



public class GameManager : MonoBehaviour, IObserver
{


    #region Structs
    [Header("Structs")]
    [SerializeField]
    public Vector2 CurrentPosition;


    #endregion

    #region Classes
    [Header("Classes")]
    [SerializeField]
    private Text timer;

    public Database Database; //Addressable Asset 사용하면 Resource.Load 보다 나을 것. Streaming Asset에 넣어도 됨.
    public ObjectPool<GameObject> SpawnPool;
    public ObjectPool<GameObject> BoxPool;

    [SerializeField]
    private GameObject background; 
    [SerializeField]
    private GameObject itemReceiveMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject deadMenu;
    [SerializeField]
    private GameObject dead;
    [SerializeField]
    private GameObject survive;
    

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private UpgradeWindow upgradeWindow;

    [SerializeField]
    private InfiniteTilemap infiniteTilemap;
    #endregion

    #region Variables
    [Header("Variables")]
    public int OriginSpawnCount;
    public int CurrentSpawnCount;
    private float currentTime;
    #endregion
    
    private void Start()
    {
        Addressables.LoadAssetAsync<Database>("Database").Completed += OnAssetLoaded;
        playerManager.RegisterObserver(this);
        
    }

    void Update()
    {
        if(BoxPool.IsUnityNull())
        {
            return;
        }    

        OriginSpawnCount = 10 + (int)currentTime / 10;
        currentTime += Time.deltaTime;
        timer.text = ((int)currentTime / 60).ToString("D2") + ":" + ((int)currentTime % 60).ToString("D2");
        if (currentTime / 60 >= Database.Stage)
        {
            Database.Stage++;
        }
        if (CurrentSpawnCount >= OriginSpawnCount)
        {
            return;
        }
        for (; CurrentSpawnCount < OriginSpawnCount; CurrentSpawnCount++)
        {
            if (CurrentSpawnCount >= OriginSpawnCount)
            {
                continue;
            }
            Enemy enemy = SpawnPool.Get().GetComponent<Enemy>();
            enemy.PlayerManager = playerManager;
            enemy.Database = Database;
            enemy.RegisterObserver(this);
        }
    }

    #region GetFile
    void OnAssetLoaded(AsyncOperationHandle<Database> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Database = obj.Result; // 로드된 자산 인스턴스화

            Time.timeScale = 1f;
            SpawnPool = new ObjectPool<GameObject>(Spawn, Respawn, Release);
            BoxPool = new ObjectPool<GameObject>(DropBox, Respawn, Release);
            upgradeWindow.RegisterObserver(this);
            upgradeWindow.PlayerManager = playerManager;    
            upgradeWindow.Database = Database;
            infiniteTilemap.Database = Database;
            playerManager.Database = Database;
            playerManager.Ready();
            CurrentPosition = Vector2.zero;
            Database.SetOrigin();
        }
        else
        {
            Debug.LogError("Failed to load Addressable Asset.");
        }
    }

    #endregion

    #region ObjectPool

    public void BoxDrop(Vector2 pos)
    {
        float dropBox = Random.Range(0, 10);
        if (dropBox <= 0f)
        {
            GameObject box = BoxPool.Get();
            box.transform.position = pos;
            box.GetComponent<BoxManager>().RegisterObserver(this);
        }
    }

    public GameObject Spawn()
    {
        int kind = UnityEngine.Random.Range(0, 5);
        GameObject go;
        //switch(kind)
        //{
        //    case 0:
        //        go = (GameObject)Resources.Load("Enemy0");
        //        break;
        //    case 1:
        //        go = (GameObject)Resources.Load("Enemy1");
        //        break;
        //    case 2:
        //        go = (GameObject)Resources.Load("Enemy2");
        //        break;
        //    case 3:
        //        go = (GameObject)Resources.Load("Enemy3");
        //        break;
        //    default:
        //        go = (GameObject)Resources.Load("Enemy4");
        //        break;
        //}
        go = (GameObject)Resources.Load("Enemy0");
        return Instantiate(go);
    }

    public void Release(GameObject go)
    {
        go.SetActive(false);
    }
    
    public void Respawn(GameObject go)
    {
        go.SetActive(true);
    }

    public GameObject DropBox()
    {
        GameObject go = (GameObject)Resources.Load("Box");
        BoxManager box = go.GetComponent<BoxManager>();
        box.Database = Database;
        box.PlayerManager = playerManager;
        
        
        return Instantiate(go);
    }

    #endregion

    #region UI
    public void TurnOnItemReceiveUI()
    {
        itemReceiveMenu.SetActive(true);
        TurnOnBackground();
    }

    public void TurnOnPauseMenu()
    {
        pauseMenu.SetActive(true);
        TurnOnBackground();
    }

    public void TurnOnBackground()
    {
        background.SetActive(true);
        Time.timeScale = 0f;
    }

    public void TurnOffBackground()
    {
        itemReceiveMenu.SetActive(false);
        pauseMenu.SetActive(false);
        background.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameEnd()
    {
        TurnOnBackground();
        deadMenu.SetActive(true);
        if (currentTime >= 1800f)
        {
            dead.SetActive(false);
            survive.SetActive(true);
            return;
        }
        dead.SetActive(true);
        survive.SetActive(false);
    }

    public void ReStart()
    {
        SceneManager.LoadSceneAsync("01_InGame");
    }

    public void ShutDown()
    {
        Application.Quit();
    }

    #endregion


    #region Observer

    public void PlayerDead()
    {
        GameEnd();
    }

    public void EnemyDead(Transform transform)
    {
        --CurrentSpawnCount;
        BoxDrop(transform.position);
    }

    public void EnemyRelease(GameObject gameObject)
    {
        SpawnPool.Release(gameObject);
    }

    public void ReleaseBox(GameObject box)
    {
       
        TurnOnItemReceiveUI();
        BoxPool.Release(box);
    }

    public void MovePosition(Vector2 _vec)
    {
        CurrentPosition += _vec;
    }
    #endregion
}
