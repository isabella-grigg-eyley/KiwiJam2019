using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CarriageSettings", menuName = "Logic/Carriage Settings", order = 1)]
[System.Serializable]
public class CarriageSettings : ScriptableObject
{
    [System.Serializable]
    public enum Color
	{
		Red,
		Green,
		Blue,
		Wild,
		Choose,
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

	[SerializeField]
	private Color m_color = Color.Red;

	public Color GetColor()
	{
		return m_color;
	}

	[SerializeField]
	private Ability m_ability = Ability.None;

	public Ability GetAbility()
	{
		return m_ability;
	}
}
