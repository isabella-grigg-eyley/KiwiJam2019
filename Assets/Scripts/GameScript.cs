using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
	public static System.Action<Beetle, Beetle> OnReadyToFight = null;

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

	public bool ReadyToFight
	{
		get
		{
			return (
				m_player1.CarriageCount >= GameConstants.MAX_HAND_SIZE &&
				m_player2.CarriageCount >= GameConstants.MAX_HAND_SIZE
			);
		}
	}

	// If false, it's player 2's turn
	private bool m_player1Turn = true;

	private float m_currentTurnTimer = 0;

	private bool m_gameplayActive = false;

	private void OnCarriageSelected(Carriage c)
	{
		if (ReadyToFight)
		{
			Debug.Log("READY TO FIGHT");
			return;
		}
		CarriageDefinition.Color color = c.CarriageDefinition.GetColor();

		if (color == CarriageDefinition.Color.Wild)
		{
			int rand = UnityEngine.Random.Range(0, 3);
			color = (CarriageDefinition.Color) rand;
		}

		CarriageDefinition.Ability ability = c.CarriageDefinition.GetAbility();

		Beetle player = GetCurrentPlayer();
		Beetle otherPlayer = GetOtherPlayer();

		switch (ability)
		{
			case CarriageDefinition.Ability.None:
				player.AddCarriage(color);
				break;
			case CarriageDefinition.Ability.Copy:
				if (player.CarriageCount == 0)
				{
					return;
				}

				Carriage lastCarriage = player.GetLastCarriage();
				color = lastCarriage.CarriageDefinition.GetColor();
				player.AddCarriage(color);
				break;
			case CarriageDefinition.Ability.Send:
				otherPlayer.AddCarriage(color);
				break;
			case CarriageDefinition.Ability.Swap:
				if (player.CarriageCount > 0 && otherPlayer.CarriageCount > 0)
				{
					Carriage c1 = player.RemoveLastCarriage();
					Carriage c2 = otherPlayer.RemoveLastCarriage();
					player.AddCarriage(c2.CarriageDefinition.GetColor());
					otherPlayer.AddCarriage(c1.CarriageDefinition.GetColor());
					Destroy(c1.gameObject);
					Destroy(c2.gameObject);
				}

				player.AddCarriage(color);
				break;
			case CarriageDefinition.Ability.DoubleDip:
				player.AddCarriage(color);
				//TODO
				break;
			case CarriageDefinition.Ability.Shuffle:
				player.AddCarriage(color);
				//TODO
				break;
		}

		c.gameObject.SetActive(false);

		NextTurn();
	}

	private Carriage CreateCarriage(CarriageDefinition def)
	{
		Carriage carriage = Instantiate(m_carriagePrefab, Vector3.zero, Quaternion.identity);
		carriage.Init(def);
		return carriage;
	}

	private void OnEnable()
	{
		MatchOutcomeController.OnMatchOutcomeDecided += OnMatchOutcomeDecided;
	}

	private void OnMatchOutcomeDecided(bool p1wins)
	{
		Debug.LogFormat("Beetle {0} wins", p1wins ? m_player1.name : m_player2.name);

		if (p1wins)
		{
			m_player1.LoseHealth();
		}
		else
		{
			m_player2.LoseHealth();
		}

		RestartRound();
	}

	private void RestartRound()
	{
		m_carriageContainer.GetComponent<CarriageMenu>().Reset();
		m_currentHand.Clear();
		m_player1.Reset();
		m_player2.Reset();
		InitializeRound();
	}

	private void Start()
	{
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
			//Carriage carriage = Instantiate<Carriage>(m_carriagePrefab, m_carriageContainer);
			//carriage.Init(m_currentHand[i]);
			Carriage carriage = CreateCarriage(m_currentHand[i]);
			carriage.OnSelect += OnCarriageSelected;
			carriage.transform.SetParent(m_carriageContainer);

			m_currentAvailableCarriages.Add(carriage);
		}

		m_carriageContainer.GetComponent<CarriageMenu>().Init();

		m_gameplayActive = true;
		TurnStart();
	}

	private void NextTurn()
	{
		if (ReadyToFight)
		{
			OnReadyToFight?.Invoke(m_player1, m_player2);
		}
		m_player1Turn = !m_player1Turn;
	}

	private void TurnStart()
	{
		m_currentTurnTimer = m_turnLength;
	}

	private Beetle GetCurrentPlayer()
	{
		if (m_player1Turn)
		{
			return m_player1;
		}
		else
		{
			return m_player2;
		}
	}

	private Beetle GetOtherPlayer()
	{
		if (m_player1Turn)
		{
			return m_player2;
		}
		else
		{
			return m_player1;
		}
	}
}
