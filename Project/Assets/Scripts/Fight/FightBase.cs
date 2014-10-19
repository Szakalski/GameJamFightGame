using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FightBase : MonoBehaviour {

	public static string E_FIGHT_START = "eFightStart";
	public static string E_SEND_HIT = "eSendHit";
	public static string E_SEND_DAMAGE = "eSendDamage";
	public static string E_PLAYER_WON = "ePlayerWon";
	public static string E_PLAYER_LOST = "ePlayerLost";

	[HideInInspector]
	public EventManager eventManager;
	private SceneManager _sceneManager;

	private Text _endGameText;
	private GameObject _leftTapArea, _rightTapArea;
		
	private void Awake()
	{
		eventManager = EventManager.instance;
		_sceneManager = SceneManager.getInstance();
		_endGameText = GameObject.Find("EndgameText").GetComponent<Text>();
		_leftTapArea = GameObject.Find("LeftHitArea");
		_rightTapArea = GameObject.Find("RightHitArea");
	}
	
	private void Start () 
	{
		_endGameText.enabled = false;
		InitiateListeners(eventManager);
		eventManager.dispatchEvent(new CustomEvent(E_FIGHT_START));
	}

	private void InitiateListeners(EventManager eventManager)
	{
		eventManager.addEventListener(E_FIGHT_START, gameObject, "FightStart");
		eventManager.addEventListener(E_SEND_HIT, gameObject, "CheckPlayerHit");
		eventManager.addEventListener(E_PLAYER_WON, gameObject, "FinishWonFight");
	}

	private void CheckPlayerHit(CustomEvent e)
	{
		PunchTarget target = (PunchTarget)e.arguments["target"];

		#if UNITY_EDITOR
			Debug.Log("Player hit area: " + target.ToString());
		#endif

		CheckTargetAndApplyDamage(target);
	}

	private void CheckTargetAndApplyDamage(PunchTarget target)
	{
		CustomEvent damageEvent = new CustomEvent(E_SEND_DAMAGE);

		if(target == PunchTarget.LEFT_LOW || target == PunchTarget.RIGHT_LOW)
			damageEvent.arguments.Add("damage", 10f);
		else if(target == PunchTarget.LEFT_MID || target == PunchTarget.RIGHT_MID)
			damageEvent.arguments.Add("damage", 20f);
		else if(target == PunchTarget.LEFT_TOP || target == PunchTarget.RIGHT_TOP)
			damageEvent.arguments.Add("damage", 30f);

		eventManager.dispatchEvent(damageEvent);
	}

	private void FinishWonFight(CustomEvent e)
	{
		_endGameText.text = "YOU WON";
		_endGameText.enabled = true;

		_leftTapArea.SetActive(false);
		_rightTapArea.SetActive(false);	

		StartCoroutine(WaitAndGoToMainMenu());
	}

	private IEnumerator WaitAndGoToMainMenu()
	{
		yield return new WaitForSeconds(2.5f);

		eventManager.removeAllEventListeners(gameObject);
		_sceneManager.loadLevel("MainMenu");
	}

	private void FightStart(CustomEvent e)
	{
		Debug.Log("EventTest");
	}
}
