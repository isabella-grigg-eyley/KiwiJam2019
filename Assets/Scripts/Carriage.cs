using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carriage : MonoBehaviour
{
	[System.Serializable]
	public enum State
	{
		Selectable,
		Attached,
	}

	[SerializeField]
	private Button m_button = null;

	private CarriageDefinition m_carriageDefinition = null;

	public CarriageDefinition CarriageDefinition
	{
		get { return m_carriageDefinition; }
	}

	[HideInInspector]
	public State CurrentState = State.Selectable;

	[SerializeField]
	private CarriageImage m_carriageImage = null;

	public void Init(CarriageDefinition c, State state = State.Selectable)
	{
		m_carriageDefinition = c;

		Debug.Log(CarriageDefinition.ToString());

		m_carriageImage.Init(c);
		m_carriageImage.SetVisibleState(state);

		m_button.enabled = state == State.Selectable;

		if (state == State.Selectable)
			m_button.onClick.AddListener(() => Select(this));
	}

	private void Select(Carriage c)
	{
		Debug.Log(string.Format("SELECTED {0}", c));
		GameScript.SelectCarriage(c);
	}
}
