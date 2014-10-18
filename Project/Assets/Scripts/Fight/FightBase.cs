using UnityEngine;
using System.Collections;

public class FightBase : MonoBehaviour {

	public static string E_FIGHT_START = "eFightStart";
	public static string E_SEND_HIT = "eSendHit";
	
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
		eventManager.addEventListener(E_SEND_HIT, gameObject, "CheckPlayerHit");
	}

	private void CheckPlayerHit(CustomEvent e)
	{
		PunchTarget target = (PunchTarget)e.arguments["target"];

		Debug.Log("Player hit area: " + target.ToString());
	}

	private void FightStart(CustomEvent e)
	{
		Debug.Log("EventTest");
	}
}
