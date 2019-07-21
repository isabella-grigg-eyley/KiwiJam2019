using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CarriageImageDatabase", menuName = "Logic/Carriage Image DB", order = 1)]
public class CarriageImageDatabase : ScriptableObject
{
    private static CarriageImageDatabase _instance = null;
    public static CarriageImageDatabase Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.Load<CarriageImageDatabase>("CarriageImageDatabase");
                if (!_instance)
                {
                    _instance = Resources.FindObjectsOfTypeAll<CarriageImageDatabase>().FirstOrDefault();
                }
                if (!_instance)
                    Debug.LogError("COULD NOT LOAD DATABASE");
            }
            return _instance;
        }
    }

    #region BASES
    public Sprite CarriageBaseRed = null;
    public Sprite CarriageBaseGreen = null;
    public Sprite CarriageBaseBlue = null;
    public Sprite CarriageBaseWild = null;
    public Sprite CarriageBaseCopy = null;
    #endregion BASES

    #region COATS
    public Sprite CarriageCoatNone = null;
    public Sprite CarriageCoatSend = null;
    public Sprite CarriageCoatShuffle = null;
    public Sprite CarriageCoatDoubleDip = null;
    public Sprite CarriageCoatSwap = null;
    #endregion COATS

    #region Attached
    public Sprite CarriageAttachedRed = null;
    public Sprite CarriageAttachedGreen = null;
    public Sprite CarriageAttachedBlue = null;
    #endregion Attached
}
