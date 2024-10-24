using Unity.VisualScripting;
using UnityEngine;

public class SpinningWeapons : MonoBehaviour
{
    public float rotateSpeed;
    public float additionalRotateSpeed;

    public Database database;
    public PlayerManager playerManager;


    private void Start()
    {
        ChangeNumberOfWeapon();
    }
    void Update()
    {

        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z - Time.deltaTime * rotateSpeed * additionalRotateSpeed);
        

    }

    private void FixedUpdate()
    {
        //transform.position = playerManager.transform.position;
    }

    public void ChangeNumberOfWeapon()
    {
        int numOfChild = transform.childCount;
        for (int i = 0; i < numOfChild; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.transform.position = Vector3.zero;
            child.transform.localEulerAngles = new Vector3(0, 0, (360 / numOfChild) * i);
            child.GetComponent<Weapons>().database= database;
        }
    }

    
}
