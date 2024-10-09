using Unity.VisualScripting;
using UnityEngine;

public class SpinningWeapons : MonoBehaviour
{
    public float radius;
    public float rotateSpeed;

    private Database database;

    private void Start()
    {
        radius = 0.9f;
        ChangeNumberOfWeapon();
        database = Database.instance;
    }
    void Update()
    {

        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z - Time.deltaTime * rotateSpeed);
        

    }

    private void FixedUpdate()
    {
        transform.position = PlayerManager.instance.transform.position;
    }

    public void ChangeNumberOfWeapon()
    {
        int numOfChild = transform.childCount;
        for (int i = 0; i < numOfChild; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.transform.position = Vector3.zero;
            child.transform.localEulerAngles = new Vector3(0, 0, (360 / numOfChild) * i);
        }
    }

    
}
