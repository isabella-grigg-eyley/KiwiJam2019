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

	[SerializeField]
	private CarriageImage m_carriageImage = null;

	public void Init(CarriageDefinition c)
    {
        m_carriageDefinition = c;

		Debug.Log(CarriageDefinition.ToString());

		m_carriageImage.Init(c);
		m_carriageImage.SetState(State.Selectable);
    }
}
