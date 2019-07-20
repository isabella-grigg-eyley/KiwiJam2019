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

    private CarriageDefinition m_carriageDefinition = null;

	public CarriageDefinition CarriageDefinition
	{
		get { return m_carriageDefinition; }
	}

	[HideInInspector]
	public State CurrentState = State.Selectable;

	// Images
	[SerializeField]
	private CarriageImage m_redImage = null;
	[SerializeField]
	private CarriageImage m_greenImage = null;
	[SerializeField]
	private CarriageImage m_blueImage = null;

	public void Init(CarriageDefinition c)
    {
        m_carriageDefinition = c;

		CarriageDefinition.Color color = m_carriageDefinition.GetColor();

		Debug.Log(CarriageDefinition.ToString());

		m_redImage.gameObject.SetActive(false);
		m_greenImage.gameObject.SetActive(false);
		m_blueImage.gameObject.SetActive(false);

		switch (color)
		{
			case CarriageDefinition.Color.Red:
				m_redImage.SetState(State.Selectable);
				m_redImage.gameObject.SetActive(true);
				break;
			case CarriageDefinition.Color.Green:
				m_greenImage.SetState(State.Selectable);
				m_greenImage.gameObject.SetActive(true);
				break;
			case CarriageDefinition.Color.Blue:
				m_blueImage.SetState(State.Selectable);
				m_blueImage.gameObject.SetActive(true);
				break;
		}
    }
}
