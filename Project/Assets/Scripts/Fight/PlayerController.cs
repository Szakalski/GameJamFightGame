using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private FightBase _fightBase;
	private float playerStamina = 100f;
	private Animator anim;

	private void Awake()
	{
		_fightBase = GameObject.Find("FightController").GetComponent<FightBase>();
	}

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	public void OnTapArea(int target)
	{
		CustomEvent hitEvent = new CustomEvent(FightBase.E_SEND_HIT);
		hitEvent.arguments.Add("target", (PunchTarget)target);

		_fightBase.eventManager.dispatchEvent(hitEvent);
	}
}
