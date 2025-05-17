using UnityEngine;

[System.Serializable]
public struct SpinningWeapon
{
    public uint Level;
    public float Damage;
    public float OriginAngularVelocity;
    public float AdditionalAngularVelocity;
}

[CreateAssetMenu(fileName = "Database", menuName = "ScriptableObjects/Database", order = 1)]
public class Database : ScriptableObject
{
    #region Components

    #endregion

    #region Variables
    [Header("Variables")]
    public float OriginVelocity;
    public float AdditionalVelocity;
    public float OriginAttack;
    public float AdditionalAttack;
    public int Stage;
    public float OriginHp;
    public float AdditionalHp;
    public SpinningWeapon Shovel;
    public SpinningWeapon Sickle;
    #endregion
    public void SetOrigin()
    {
        Stage = 1;
        OriginVelocity = 0.2f;
        AdditionalVelocity = 1;
        OriginAttack = 50;
        AdditionalAttack = 1;
        OriginHp = 100;
        AdditionalHp = 1;

        #region Shovel
        Shovel = new SpinningWeapon();
        Shovel.Level = 1;
        Shovel.Damage = 1f;
        Shovel.OriginAngularVelocity = 180;
        Shovel.AdditionalAngularVelocity = 1f;
        #endregion

        #region Sickle
        Sickle = new SpinningWeapon();
        Sickle.Level = 0;
        Sickle.Damage = 1f;
        Sickle.OriginAngularVelocity = 180;
        Sickle.AdditionalAngularVelocity = 1f;
        #endregion
    }
}
