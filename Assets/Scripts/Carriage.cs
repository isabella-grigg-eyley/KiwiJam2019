using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Carriage : MonoBehaviour, IPointerEnterHandler
{
	public System.Action<Carriage> OnSelect = null;

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

	public void OnPointerEnter(PointerEventData eventData)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/ButtonSelect01");
    }

	private void Select(Carriage c)
	{
		Debug.Log(string.Format("SELECTED {0}", c));
		OnSelect?.Invoke(this);

		FMODUnity.RuntimeManager.PlayOneShot("event:/UI/ButtonSubmit01");
	}
}
