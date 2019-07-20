﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarriageImage : MonoBehaviour
{
	[SerializeField]
	private Image m_baseImage = null;

	[SerializeField]
	private Image m_coatImage = null;

	[SerializeField]
	private Image m_attachedImage = null;

	public void Init(CarriageDefinition definition)
	{
		switch (definition.GetColor())
		{
			case CarriageDefinition.Color.Red:
				{
					m_baseImage.sprite = CarriageImageDatabase.Instance.CarriageBaseRed;
					m_attachedImage.sprite = CarriageImageDatabase.Instance.CarriageAttachedRed;
				}
				break;
			case CarriageDefinition.Color.Green:
				{
					m_baseImage.sprite = CarriageImageDatabase.Instance.CarriageBaseGreen;
					m_attachedImage.sprite = CarriageImageDatabase.Instance.CarriageAttachedGreen;
				}
				break;
			case CarriageDefinition.Color.Blue:
				{
					m_baseImage.sprite = CarriageImageDatabase.Instance.CarriageBaseBlue;
					m_attachedImage.sprite = CarriageImageDatabase.Instance.CarriageAttachedBlue;
				}
				break;
			case CarriageDefinition.Color.Wild:
				{
					m_baseImage.sprite = CarriageImageDatabase.Instance.CarriageBaseWild;
				}
				break;
			case CarriageDefinition.Color.None:
				{
					m_baseImage.sprite = CarriageImageDatabase.Instance.CarriageBaseCopy;
				}
				break;
		}

		switch (definition.GetAbility())
		{
			case CarriageDefinition.Ability.None:
				{
					m_coatImage.sprite = CarriageImageDatabase.Instance.CarriageCoatNone;
				}
				break;
			case CarriageDefinition.Ability.Copy:
				{
					m_coatImage.sprite = CarriageImageDatabase.Instance.CarriageCoatNone;
				}
				break;
			case CarriageDefinition.Ability.DoubleDip:
				{
					m_coatImage.sprite = CarriageImageDatabase.Instance.CarriageCoatDoubleDip;
				}
				break;
			case CarriageDefinition.Ability.Send:
				{
					m_coatImage.sprite = CarriageImageDatabase.Instance.CarriageCoatSend;
				}
				break;
			case CarriageDefinition.Ability.Shuffle:
				{
					m_coatImage.sprite = CarriageImageDatabase.Instance.CarriageCoatShuffle;
				}
				break;
			case CarriageDefinition.Ability.Swap:
				{
					m_coatImage.sprite = CarriageImageDatabase.Instance.CarriageCoatSwap;
				}
				break;
		}
	}

	public void SetVisibleState(Carriage.State state)
	{
		switch (state)
		{
			case Carriage.State.Selectable:
				{
					m_baseImage.gameObject.SetActive(true);
					m_coatImage.gameObject.SetActive(true);
					m_attachedImage.gameObject.SetActive(false);
				}
				break;
			case Carriage.State.Attached:
				{
					m_baseImage.gameObject.SetActive(false);
					m_coatImage.gameObject.SetActive(false);
					m_attachedImage.gameObject.SetActive(true);
				}
				break;
		}
	}

	public void SetPosition(bool left)
	{
		Vector3 scale = m_attachedImage.transform.localScale;
		scale.x = left ? -1 : 1;
		m_attachedImage.transform.localScale = scale;
	}
}
