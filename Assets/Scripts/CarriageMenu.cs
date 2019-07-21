using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageMenu : MonoBehaviour
{
	public void Init()
    {
        for (int i = 0; i < transform.childCount; i++)
		{
			RectTransform child = transform.GetChild(i).GetComponent<RectTransform>();

			if (child == null)
			{
				continue;
			}
			
			child.Rotate(0, 0, (360 / transform.childCount * i) + 180);
		}
	}
}
