using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu (fileName = "DeckGenerator", menuName = "Logic/Deck Generator", order = 1)]
public class DeckGenerator : ScriptableObject
{
    [SerializeField]
    private int m_deckCount = 9;

    [SerializeField]
    private List<CarriageSettings> m_carriageList = new List<CarriageSettings> ();

    [SerializeField]
    private List<CarriageSettings> m_deck = new List<CarriageSettings> ();

    public List<CarriageSettings> GetDeck ()
    {
        return m_deck;
    }

    //public Dictionary<CarriageSettings, int>

    //public struct CarriagePair
    //{
    //    public CarriageSettings Type;

    //}

    /// <summary>
    /// Creates the deck
    /// </summary>
    public void Generate ()
    {
        m_deck.Clear ();

        for (int i = 0; i < m_deckCount; ++i)
        {
            CarriageSettings c = GetRandomCarriage ();
            m_deck.Add (c);
        }

        Shuffle();
    }

    private CarriageSettings GetRandomCarriage ()
    {
        return m_carriageList[Random.Range (0, m_carriageList.Count)];
    }

    public void Shuffle()
    {
        m_deck.Shuffle();
    }
}

[CustomEditor(typeof(DeckGenerator))]
public class DeckGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DeckGenerator deck = (DeckGenerator)target;

        if (DrawDefaultInspector())
        {

        }

        if (GUILayout.Button("Generate"))
        {
            deck.Generate();
        }

        if (GUILayout.Button("Shuffle"))
        {
            deck.Shuffle();
        }
    }
}
