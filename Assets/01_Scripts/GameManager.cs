using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance.IsUnityNull())
            instance = this;
        else
            Destroy(gameObject);
    }

    public int originSpawnCount;
    public int currentSpawnCount;
    private float currentTime;

    [SerializeField]
    private Text timer;
    public Database database;
    public ObjectPool<GameObject> spawnPool;
    public ObjectPool<GameObject> boxPool;

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

    private void Start()
    {
        Time.timeScale = 1f;
        spawnPool = new ObjectPool<GameObject>(Spawn, Respawn, Release);
        boxPool = new ObjectPool<GameObject>(DropBox, Respawn, Release);

        database.SetOrigin();
    }

    void Update()
    {
        originSpawnCount = 10 + (int)currentTime / 10;
        currentTime += Time.deltaTime;
        timer.text = ((int)currentTime / 60).ToString("D2") + ":" + ((int)currentTime % 60).ToString("D2");
        if (currentSpawnCount < originSpawnCount)
        {
            for (int i = currentSpawnCount; i <= originSpawnCount; i++)
            {
                currentSpawnCount++;
                Enemy enemy = spawnPool.Get().GetComponent<Enemy>();
            }
        }
        if (currentTime / 60 >= database.stage)
        {
            database.stage++;
        }

        
    }

    public void BoxDrop(Vector2 pos)
    {
        float dropBox = Random.Range(0, 10);
        if (dropBox <= 0f)
        {
            GameObject box = boxPool.Get();
            box.transform.position = pos;
        }
    }

    #region Enemy Object Pooling
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
    #endregion

    #region Box Object Pooling
    public GameObject DropBox()
    {
        GameObject go = (GameObject)Resources.Load("Box");
        return Instantiate(go);
    }

    #endregion

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
        if (GameManager.instance.currentTime >= 1800f)
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
}
