using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "DeckGenerator", menuName = "Logic/Deck Generator", order = 1)]
public class DeckGenerator : ScriptableObject
{
	[SerializeField]
	private int m_handSize = 9;

	[SerializeField]
	private List<CarriagePair> m_carriageList = new List<CarriagePair>();

	[SerializeField, FormerlySerializedAs("m_deck")]
	private List<CarriageDefinition> m_hand = new List<CarriageDefinition>();

	public List<CarriageDefinition> GetHand()
	{
		return m_hand;
	}

	[Serializable]
	public struct CarriagePair
	{
		public CarriageDefinition Settings;
		public int Count;
	}

	private List<CarriageDefinition> m_deck = null;

	public void ConstructPool()
	{
		m_deck = new List<CarriageDefinition>();

		for (int i = 0; i < m_carriageList.Count; i++)
		{
			CarriagePair pair = m_carriageList[i];
			for (int j = 0; j < pair.Count; j++)
			{
				m_deck.Add(pair.Settings);
			}
		}
	}

	/// <summary>
	/// Creates the hand
	/// </summary>
	public void Generate(List<CarriageDefinition> exclude = null)
	{
		m_hand.Clear();

		List<CarriageDefinition> filteredDeck = new List<CarriageDefinition>();
		if (exclude == null)
		{
			filteredDeck = m_deck;
		}
		else
		{
			for (int i = 0; i < m_deck.Count; i++)
			{
				CarriageDefinition c = m_deck[i];
				if (!exclude.Contains(c))
				{
					filteredDeck.Add(c);
				}
			}
		}

		for (int i = 0; i < m_handSize; ++i)
		{
			CarriageDefinition c = GetRandomCarriage(filteredDeck);
			m_hand.Add(c);
		}

		Shuffle();
	}

	private CarriageDefinition GetRandomCarriage(List<CarriageDefinition> deck)
	{
		int index = UnityEngine.Random.Range(0, deck.Count);

		return deck[index];
	}

	public void Shuffle()
	{
		m_hand.Shuffle();
	}
}

[CustomEditor(typeof(DeckGenerator))]
public class DeckGeneratorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DeckGenerator deck = (DeckGenerator) target;

		if (DrawDefaultInspector())
		{

		}

		if (GUILayout.Button("Generate"))
		{
			deck.ConstructPool();
			deck.Generate();
		}

		if (GUILayout.Button("Shuffle"))
		{
			deck.Shuffle();
		}
	}
}
