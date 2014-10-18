using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	private SceneManager _sceneManager;

	private void Awake()
	{
		_sceneManager = SceneManager.getInstance();
	}

	public void OnStartButtonClick()
	{
		_sceneManager.loadLevel("FightScene");
	}
}
