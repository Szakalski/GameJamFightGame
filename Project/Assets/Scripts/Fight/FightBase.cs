using UnityEngine;
using System.Collections;

public class FightBase : MonoBehaviour {

	public static string E_FIGHT_START = "eFightStart";

	[HideInInspector]
	public EventManager eventManager;
	private SceneManager _sceneManager;

	private void Awake()
	{
		eventManager = EventManager.instance;
		_sceneManager = SceneManager.getInstance();
	}
	
	private void Start () {
		InitiateListeners(eventManager);
		eventManager.dispatchEvent(new CustomEvent(E_FIGHT_START));
	}

	private void InitiateListeners(EventManager eventManager)
	{
		eventManager.addEventListener(E_FIGHT_START, gameObject, "FightStart");
	}

	private void FightStart()
	{
		Debug.Log("EventTest");
	}
}
