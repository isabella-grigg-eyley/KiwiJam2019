using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = CarriageDefinition.Color;

public class Beetle : MonoBehaviour
{
    private int m_health = GameConstants.MAX_HAND_SIZE;

    public int Health
    {
        get
        {
            return m_health;
        }
        set
        {
            Debug.Log(string.Format("Beetle {0} has {1} health", value));
            m_health = value;
        }
    }

    [SerializeField]
    private List<CarriageDefinition> m_carriageList = new List<CarriageDefinition>();

    public int CarriageCount
    {
        get { return m_carriageList.Count; }
    }

    public void Reset()
    {
        Health = GameConstants.MAX_BEETLE_HP;
    }

    public void Init()
    {
        ClearCarriage();
    }

    public void LoseHealth()
    {
        Health--;
    }

    public void AddCarriage(CarriageDefinition carriage)
    {
        if (CarriageCount >= GameConstants.MAX_HAND_SIZE)
        {
            Debug.LogWarning(string.Format("Beetle {0} carriage count is maxed out", this.name));
            return;
        }

        Debug.Log(string.Format("Beetle {0} added carriage {1}", this.name, carriage));
        m_carriageList.Add(carriage);
    }

    public void ClearCarriage()
    {
        Debug.Log(string.Format("Beetle {0} cleared its carriage", this.name));
        m_carriageList.Clear();
    }

    public Color GetDominantColor()
    {
        Dictionary<Color, int> m_colorsFound = new Dictionary<Color, int>();

        foreach (CarriageDefinition cs in m_carriageList)
        {
            Color col = cs.GetColor();

            // Dictionary does not contain colour value
            if (!m_colorsFound.ContainsKey(col))
            {
                m_colorsFound.Add(col, 1);
            }
            // If it's already found, means we
            else
            {
                m_colorsFound[col]++;
            }
        }

        foreach (var c in m_colorsFound)
        {
            if (c.Value >= GameConstants.DOMINANT_COLOR_AMOUNT)
            {
                Debug.Log(string.Format("Beetle {0} returned dominant color: {1}", this.name, c.Key));
                return c.Key;
            }
        }

        Debug.Log(string.Format("Beetle {0} did not return a dominant color", this.name));
        return Color.None;
    }

#if UNITY_EDITOR
    #region TEST
    [ContextMenu("Create Test Carriage (RRB)")]
    public void TestCarriageRRB()
    {
        m_carriageList.Clear();
        AddCarriage(new CarriageDefinition(
            Color.Red,
            CarriageDefinition.Ability.None
        ));

        AddCarriage(new CarriageDefinition(
            Color.Red,
            CarriageDefinition.Ability.None
        ));

        AddCarriage(new CarriageDefinition(
            Color.Blue,
            CarriageDefinition.Ability.None
        ));

        Color dominant = GetDominantColor();
        Debug.Assert(dominant == Color.Red, "Dominant color for RRB is not red");
    }

    [ContextMenu("Create Test Carriage (RGB)")]
    public void TestCarriageRGB()
    {
        ClearCarriage();
        AddCarriage(new CarriageDefinition(
            Color.Red,
            CarriageDefinition.Ability.None
        ));

        AddCarriage(new CarriageDefinition(
            Color.Green,
            CarriageDefinition.Ability.None
        ));

        AddCarriage(new CarriageDefinition(
            Color.Blue,
            CarriageDefinition.Ability.None
        ));

        Color dominant = GetDominantColor();
        Debug.Assert(dominant == Color.None, "Dominant color for RGB is not none");
    }

    [ContextMenu("Create Test Carriage (RGGR)")]
    public void TestCarriageRGGR()
    {
        ClearCarriage();
        AddCarriage(new CarriageDefinition(
            Color.Red,
            CarriageDefinition.Ability.None
        ));

        AddCarriage(new CarriageDefinition(
            Color.Green,
            CarriageDefinition.Ability.None
        ));

        AddCarriage(new CarriageDefinition(
            Color.Green,
            CarriageDefinition.Ability.None
        ));

        AddCarriage(new CarriageDefinition(
            Color.Red,
            CarriageDefinition.Ability.None
        ));

        Debug.Assert(CarriageCount == GameConstants.MAX_HAND_SIZE);

        Color dominant = GetDominantColor();
        Debug.Assert(dominant == Color.Green, "Dominant color for RGGB is not green");
    }
    #endregion TEST
#endif
}
