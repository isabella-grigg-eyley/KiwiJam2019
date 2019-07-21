using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
	public static System.Action<Beetle, Beetle> OnReadyToFight = null;

	[System.Serializable]
	public class Visuals
	{
		public GameObject P1Turnlights = null;
		public GameObject P2Turnlights = null;

		public List<GameObject> P1Points = null;
		public List<GameObject> P2Points = null;
	}

	[SerializeField]
	private Visuals m_visuals = null;

	[SerializeField]
	private TextMeshProUGUI m_winnerText = null;

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
	[SerializeField]
	private Text m_timerText = null;

	private List<CarriageDefinition> m_currentHand = null;

	private List<Carriage> m_currentAvailableCarriages = null;

	public bool ReadyToFight
	{
		get
		{
			return (
				m_player1.CarriageCount >= GameConstants.MAX_CARRIAGE_CAPACITY &&
				m_player2.CarriageCount >= GameConstants.MAX_CARRIAGE_CAPACITY
			);
		}
	}

	// If false, it's player 2's turn
	private bool m_player1Turn = true;

	private float m_currentTurnTimer = 0;

	private bool m_gameplayActive = false;

	private bool m_doubleDip = false;

	private void OnCarriageSelected(Carriage c)
	{
		if (ReadyToFight)
		{
			Debug.Log("READY TO FIGHT");
			OnReadyToFight?.Invoke(m_player1, m_player2);
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
				if (otherPlayer.CarriageCount == GameConstants.MAX_CARRIAGE_CAPACITY)
				{
					player.AddCarriage(color);
				}
				else
				{
					otherPlayer.AddCarriage(color);
				}
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
				m_doubleDip = true;
				player.AddCarriage(color);
				break;
			case CarriageDefinition.Ability.Shuffle:
				player.AddCarriage(color);
				m_carriageContainer.GetComponent<CarriageMenu>().Reset();
				m_currentHand.Clear();
				DrawHand();
				m_carriageContainer.GetComponent<CarriageMenu>().Init();
				break;
		}

		c.gameObject.SetActive(false);

		if (m_doubleDip)
		{
			m_doubleDip = false;
		}
		else
		{
			NextTurn();
		}

		if (ReadyToFight)
		{
			OnReadyToFight?.Invoke(m_player1, m_player2);
		}
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
			m_player1.GainPoint();
		}
		else
		{
			m_player2.GainPoint();
		}

		// Fill all points for player 1
		for (int i = 0; i < m_player1.Points; ++i)
		{
			m_visuals.P1Points[i].SetActive(true);
		}

		// Fill all points for player 2
		for (int i = 0; i < m_player2.Points; ++i)
		{
			m_visuals.P2Points[i].SetActive(true);
		}

		// Handle the match/game winner
		string text = string.Format("Player {0}", p1wins ? "1" : "2");

		bool matchEnded = false;
		if (
			m_player1.Points >= GameConstants.POINTS_TO_WIN ||
			m_player2.Points >= GameConstants.POINTS_TO_WIN)
		{
			matchEnded = true;
			text += " is the winner!";
		}
		else
		{
			text += " won this round!";
		}

		m_winnerText.text = text;

		m_winnerText.DOFade(1, 0.35f);
		m_winnerText.rectTransform.DOScale(Vector3.zero, 2).From().SetEase(Ease.OutBack).OnComplete(() =>
		{
			
			FMODUnity.RuntimeManager.PlayOneShot("event:/Character/AttackWhistle");

			m_winnerText.DOFade(0, 0.15f).SetDelay(1f).OnComplete(() =>
			{
				if (matchEnded)
				{
					MusicController.MC.GameStartedMusic();

					SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
				}
				else
				{
					RestartRound();
				}
			});
		});
	}

	private void RestartRound()
	{
		m_carriageContainer.GetComponent<CarriageMenu>().Reset();
		m_currentHand.Clear();
		m_player1.Reset();
		m_player2.Reset();
		m_generator.ConstructDeck();
		InitializeRound(false);
	}

	private void Start()
	{
		foreach (GameObject go in m_visuals.P1Points)
		{
			go.SetActive(false);
		}

		foreach (GameObject go in m_visuals.P2Points)
		{
			go.SetActive(false);
		}

		m_winnerText.text = "Get ready to choo!";
		m_generator.ConstructDeck();
		InitializeRound(true);
	}

	private void Update()
	{
		m_currentTurnTimer -= Time.deltaTime;
		m_timerText.text = Mathf.CeilToInt(m_currentTurnTimer).ToString();

		if (m_currentTurnTimer <= 0)
		{
			NextTurn();
		}
	}

	private void InitializeRound(bool animate)
	{
		DrawHand();

		m_gameplayActive = true;
		TurnStart();

		if (!animate)
			return;

		m_winnerText.DOFade(1, 0.35f);
		m_winnerText.rectTransform.DOScale(Vector3.zero, 1).From().SetEase(Ease.OutBack).OnComplete(() =>
		{
			m_winnerText.DOFade(0, 0.15f).SetDelay(1f);
		});
	}

	private void DrawHand()
	{
		m_generator.Generate();
		m_currentHand = m_generator.GetHand();

		m_currentAvailableCarriages = new List<Carriage>();
		for (int i = 0; i < m_currentHand.Count; i++)
		{
			Carriage carriage = CreateCarriage(m_currentHand[i]);
			carriage.OnSelect += OnCarriageSelected;
			carriage.transform.SetParent(m_carriageContainer);

			m_currentAvailableCarriages.Add(carriage);
		}

		m_carriageContainer.GetComponent<CarriageMenu>().Init();
	}

	private void NextTurn()
	{
		if (m_player1.CarriageCount == 3 && m_player1.CarriageCount == m_player2.CarriageCount)
		{
			return;
		}

		if (m_player1.CarriageCount == 3)
		{
			m_player1Turn = false;
		}
		else if (m_player2.CarriageCount == 3)
		{
			m_player1Turn = true;
		}
		else
		{
			m_player1Turn = !m_player1Turn;
		}

		TurnStart();
	}

	private void TurnStart()
	{
		m_currentTurnTimer = m_turnLength;
		m_visuals.P1Turnlights.SetActive(m_player1Turn);
		m_visuals.P2Turnlights.SetActive(!m_player1Turn);
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
