using UnityEngine;
public struct SpinningWeapon
{
    public uint level;
    public float damage;
    public float originRadius;
    public float additionalRadius;
    public float originAngularVelocity;
    public float additionalAngularVelocity;
}
public class Database : MonoBehaviour
{
    public static Database instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    
    #region Components

    #endregion

    #region Variables
    [Header("Variables")]
    public float originVelocity;
    public float additionalVelocity;
    public float attack;
    public int stage;
    public float originHp;
    public float additionalHp;
    public SpinningWeapon shovel;
    public SpinningWeapon sickle;

    private void Start()
    {
        stage = 1;
        originVelocity = 0.2f;
        additionalVelocity = 1;
        attack = 10;
        originHp = 100;
        #region Shovel
        shovel = new SpinningWeapon();
        shovel.level= 0;
        shovel.damage = 1.2f;
        shovel.originRadius = 0.9f;
        shovel.additionalRadius = 1f;
        shovel.originAngularVelocity = 180;
        shovel.additionalAngularVelocity = 1f;
        #endregion

        #region Sickle
        sickle = new SpinningWeapon();
        sickle.level= 0;
        sickle.damage = 1.1f;
        sickle.originRadius = 0.9f;
        sickle.additionalRadius = 1f;
        sickle.originAngularVelocity = 210;
        sickle.additionalAngularVelocity = 1f;
        #endregion
    }
    #endregion
}
