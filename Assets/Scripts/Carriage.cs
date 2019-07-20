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

    private Button m_button = null;

    private CarriageDefinition m_carriageDefinition = null;

	public CarriageDefinition CarriageDefinition
	{
		get { return m_carriageDefinition; }
	}

    private Image m_image = null;

    public Button Button
    {
        get { return m_button; }
    }

	[HideInInspector]
	public State CurrentState = State.Selectable;

    void Awake()
    {
        m_image = GetComponent<Image>();
        m_button = GetComponent<Button>();
    }

    public void Init(CarriageDefinition c)
    {
        m_carriageDefinition = c;
    }
}
