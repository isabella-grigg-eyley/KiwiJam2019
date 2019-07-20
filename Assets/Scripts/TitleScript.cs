using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
	void Start ()
	{

	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Return))
		{
			SceneManager.LoadScene ("Game", LoadSceneMode.Single);			
		}
	}
}
