using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private FightBase _fightBase;
	private float playerStamina = 100f;

	private Animator anim;
	int attackMidRight = Animator.StringToHash("AttackMidR");
	int attackMidLeft = Animator.StringToHash("AttackMidL");

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

		if (PunchTarget.RIGHT_MID == (PunchTarget)target)
		{
			anim.SetTrigger(attackMidRight);
		}
		else if (PunchTarget.LEFT_MID == (PunchTarget)target)
		{
			anim.SetTrigger(attackMidLeft);
		}
	}
}
