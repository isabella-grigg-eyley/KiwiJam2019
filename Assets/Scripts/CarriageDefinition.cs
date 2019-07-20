using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarriageSettings", menuName = "Logic/Carriage Settings", order = 1)]
[System.Serializable]
public class CarriageDefinition : ScriptableObject
{
    [System.Serializable]
    public enum Color
    {
        Red,
        Green,
        Blue,
        Wild,
        None,
    }

    [System.Serializable]
    public enum Ability
    {
        None,
        Send,
        Swap,
        Copy,
        DoubleDip,
        Suffle,
    }

    public CarriageDefinition()
    {
        m_color = Color.None;
        m_ability = Ability.None;
    }

    public CarriageDefinition(Color c, Ability a)
    {
        m_color = c;
        m_ability = a;
    }

    [SerializeField]
    private Color m_color = Color.Red;

    [SerializeField]
    private Ability m_ability = Ability.None;

    [SerializeField]
    private GameObject m_gameObject = null;

    public Color GetColor()
    {
        return m_color;
    }

    public Ability GetAbility()
    {
        return m_ability;
    }

    public GameObject GetGameObject()
    {
        return m_gameObject;
    }

    public override string ToString()
    {
        return string.Format("Carriage (Colour: {0} - Ability: {1})", m_color.ToString(), m_ability.ToString());
    }
}
