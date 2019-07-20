using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
	[SerializeField]
	private float m_turnLength = GameConstants.START_TURN_LENGTH;

	[SerializeField]
	private DeckGenerator m_generator = null;
	[SerializeField]
	private Beetle m_player1 = null;
	[SerializeField]
	private Beetle m_player2 = null;
	[SerializeField]
	private Carriage m_carriagePrefab = null;
	[SerializeField]
	private Transform m_carriageContainer = null;

	private List<CarriageDefinition> m_currentHand = null;

	private List<Carriage> m_currentAvailableCarriages = null;

	// If false, it's player 2's turn
	private bool m_player1Turn = true;

	private float m_currentTurnTimer = 0;

	private bool m_gameplayActive = false;

	private void Start()
	{
		CarriageImageDatabase db = CarriageImageDatabase.Instance;
		m_generator.ConstructDeck();
		InitializeRound();
	}

	private void Update()
	{
		if (m_currentTurnTimer <= 0)
		m_currentTurnTimer -= Time.deltaTime;
	}

	private void InitializeRound()
	{
		m_generator.Generate();
		m_currentHand = m_generator.GetHand();

		m_currentAvailableCarriages = new List<Carriage>();
		for (int i = 0; i < m_currentHand.Count; i++)
		{
			Carriage carriage = Instantiate<Carriage>(m_carriagePrefab, m_carriageContainer);
			carriage.Init(m_currentHand[i]);

			m_currentAvailableCarriages.Add(carriage);
		}

		m_carriageContainer.GetComponent<CarriageMenu>().Init();

		m_gameplayActive = true;
		TurnStart();
	}

	private void NextTurn()
	{
		m_player1Turn = !m_player1Turn;
	}

	private void TurnStart()
	{
		m_currentTurnTimer = m_turnLength;
	}
}
