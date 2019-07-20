using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CarriageImageDatabase", menuName = "Logic/Carriage Image DB", order = 1)]
public class CarriageImageDatabase : SingletonScriptableObject<CarriageImageDatabase>
{
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
