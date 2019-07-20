using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarriageImage : MonoBehaviour
{
	[SerializeField]
	private Sprite m_topSprite = null;

	[SerializeField]
	private Sprite m_sideSprite = null;

	public void SetState(Carriage.State state)
	{
		Image image = GetComponent<Image>();

		switch(state)
		{
			case Carriage.State.Selectable:
				image.sprite = m_topSprite;
				break;
			case Carriage.State.Attached:
				image.sprite = m_sideSprite;
				break;
		}
	}
}
