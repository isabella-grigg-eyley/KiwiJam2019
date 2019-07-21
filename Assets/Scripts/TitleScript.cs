using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			OnStartGame();
		}
	}

	public void OnStartGame()
	{
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}

	public void OnInstructions()
	{

	}
}
