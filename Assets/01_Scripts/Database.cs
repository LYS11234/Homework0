using UnityEngine;

[System.Serializable]
public struct SpinningWeapon
{
    public uint level;
    public float damage;
    public float originAngularVelocity;
    public float additionalAngularVelocity;
}

[CreateAssetMenu(fileName = "Database", menuName = "ScriptableObjects/Database", order = 1)]
public class Database : ScriptableObject
{
    public static Database instance;

   

    
    #region Components

    #endregion

    #region Variables
    [Header("Variables")]
    public float originVelocity;
    public float additionalVelocity;
    public float originAttack;
    public float additionalAttack;
    public int stage;
    public float originHp;
    public float additionalHp;
    public SpinningWeapon shovel;
    public SpinningWeapon sickle;

    public void SetOrigin()
    {
        stage = 1;
        originVelocity = 0.2f;
        additionalVelocity = 1;
        originAttack = 50;
        additionalAttack = 1;
        originHp = 100;
        additionalHp = 1;
        #region Shovel
        shovel = new SpinningWeapon();
        shovel.level = 1;
        shovel.damage = 1f;
        shovel.originAngularVelocity = 180;
        shovel.additionalAngularVelocity = 1f;
        #endregion

        #region Sickle
        sickle = new SpinningWeapon();
        sickle.level = 0;
        sickle.damage = 1f;
        sickle.originAngularVelocity = 180;
        sickle.additionalAngularVelocity = 1f;
        #endregion
    }
    #endregion
}
