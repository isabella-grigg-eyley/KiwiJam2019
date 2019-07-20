using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu]
	class CarriageSettings : ScriptableObject
	{
		public enum Color
		{
			Red,
			Green,
			Blue,
			Wild,
			Choose,
		}

		public enum Ability
		{
			None,
			Swap,
			Give,
		}

		[SerializeField]
		private Color m_color = Color.Red;

		[SerializeField]
		private Ability m_ability = Ability.None;
	}
}
