using Unity.VisualScripting;
using UnityEngine;

public class SpinningWeapons : MonoBehaviour
{
    public float RotateSpeed;
    public float AdditionalRotateSpeed;

    public Database Database;
    public PlayerManager PlayerManager;


    private void Start()
    {
        ChangeNumberOfWeapon();
    }
    void Update()
    {

        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z - Time.deltaTime * RotateSpeed * AdditionalRotateSpeed);
        

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
            child.GetComponent<Weapons>().Database= Database;
        }
    }

    
}
