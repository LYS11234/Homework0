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

    public int OriginSpawnCount;
    public int CurrentSpawnCount;
    private float _currentTime;

    [SerializeField]
    private Text _timer;

    public Database Database; //Addressable Asset 사용하면 Resource.Load 보다 나을 것. Streaming Asset에 넣어도 됨.
    public ObjectPool<GameObject> SpawnPool;
    public ObjectPool<GameObject> BoxPool;

    [SerializeField]
    private GameObject _background; 
    [SerializeField]
    private GameObject _itemReceiveMenu;
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private GameObject _deadMenu;
    [SerializeField]
    private GameObject _dead;
    [SerializeField]
    private GameObject _survive;
    
    private Observer observer;

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private UpgradeWindow upgradeWindow;

    [SerializeField]
    private InfiniteTilemap infiniteTilemap;

    private void Start()
    {
        Addressables.LoadAssetAsync<Database>("Database").Completed += OnAssetLoaded;
        playerManager.RegisterObserver(this);
        playerManager.database = Database;
    }

    void Update()
    {
        if(BoxPool.IsUnityNull())
        {
            return;
        }    

        OriginSpawnCount = 10 + (int)_currentTime / 10;
        _currentTime += Time.deltaTime;
        _timer.text = ((int)_currentTime / 60).ToString("D2") + ":" + ((int)_currentTime % 60).ToString("D2");
        if (_currentTime / 60 >= Database.stage)
        {
            Database.stage++;
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
            enemy.playerManager = playerManager;
            enemy.database = Database;
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
            upgradeWindow.playerManager = playerManager;    
            upgradeWindow.Database = Database;
            infiniteTilemap.database = Database;
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
        box.database = Database;
        box.playerManager = playerManager;
        
        
        return Instantiate(go);
    }

    #endregion

    #region UI
    public void TurnOnItemReceiveUI()
    {
        _itemReceiveMenu.SetActive(true);
        TurnOnBackground();
    }

    public void TurnOnPauseMenu()
    {
        _pauseMenu.SetActive(true);
        TurnOnBackground();
    }

    public void TurnOnBackground()
    {
        _background.SetActive(true);
        Time.timeScale = 0f;
    }

    public void TurnOffBackground()
    {
        _itemReceiveMenu.SetActive(false);
        _pauseMenu.SetActive(false);
        _background.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameEnd()
    {
        TurnOnBackground();
        _deadMenu.SetActive(true);
        if (_currentTime >= 1800f)
        {
            _dead.SetActive(false);
            _survive.SetActive(true);
            return;
        }
        _dead.SetActive(true);
        _survive.SetActive(false);
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

    #endregion
}
