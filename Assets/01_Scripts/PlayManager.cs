using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

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
    private Database database;
    public ObjectPool<GameObject> spawnPool;
    public ObjectPool<GameObject> boxPool;

    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject itemReceiveMenu;
    [SerializeField]
    private GameObject pauseMenu;

    private void Start()
    {
        database = Database.instance;
        spawnPool = new ObjectPool<GameObject>(Spawn, Respawn, Release);
        boxPool = new ObjectPool<GameObject>(DropBox, Respawn, Release);
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
                EnemyManager enemy = spawnPool.Get().GetComponent<EnemyManager>();
            }
        }
        if (currentTime / 60 >= database.stage)
        {
            database.stage++;
        }

        float dropBox = Random.Range(0, 1000);
        Debug.Log(dropBox);
        if (dropBox <= 0f)
        {
            GameObject box = boxPool.Get();
            box.transform.position = new Vector2(Random.Range(-50, 50.0f), Random.Range(-50, 50.0f));
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
        pauseMenu.SetActive(true);
        background.SetActive(false);
        Time.timeScale = 1f;
    }
}
