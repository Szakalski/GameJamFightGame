using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private FightBase _fightBase;
	private float playerStamina = 100f;

	private DodgeState _playerDodge;

	private Animator anim;
	int attackMidRight = Animator.StringToHash("AttackMidR");
	int attackMidLeft = Animator.StringToHash("AttackMidL");
//	int attackTopRight = Animator.StringToHash("AttackMidL");
//	int attackTopLeft = Animator.StringToHash("AttackMidL");
//	int attackLowLeft = Animator.StringToHash("AttackMidL");
//	int attackLowLeft = Animator.StringToHash("AttackMidL");

	int dodgeLeft = Animator.StringToHash("DodgeLeft");
	int dodgeRight = Animator.StringToHash("DodgeRight");
	int dodgeReturn = Animator.StringToHash("DodgeReturn");
	
	public DodgeState PlayerDodge
	{
		get
		{
			return _playerDodge;
		}
	}

	private void Awake()
	{
		_fightBase = GameObject.Find("FightController").GetComponent<FightBase>();
	}

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		CheckGyroscopeDodge(Input.acceleration);
	}

	private void CheckGyroscopeDodge(Vector3 acceleration)
	{
		if(acceleration.normalized.x < -0.35f)
			_playerDodge = DodgeState.DODGE_LEFT;
		else if(acceleration.normalized.x > 0.35f)
			_playerDodge = DodgeState.DODGE_RIGHT;
		else
			_playerDodge = DodgeState.NO_DODGE;

		SetDodgeAnimation(_playerDodge);
	}

	private void SetDodgeAnimation(DodgeState playerDodge)
	{
		if(_playerDodge == DodgeState.NO_DODGE)
		{			
			anim.ResetTrigger(dodgeLeft);
			anim.ResetTrigger(dodgeRight);
			anim.SetTrigger(dodgeReturn);
		}
		else if(_playerDodge == DodgeState.DODGE_LEFT)
		{
			anim.ResetTrigger(dodgeReturn);
			anim.SetTrigger(dodgeLeft);
		}
		else if(_playerDodge == DodgeState.DODGE_RIGHT)
		{
			anim.ResetTrigger(dodgeReturn);
			anim.SetTrigger(dodgeRight);
		}
	}

	public void OnTapArea(int target)
	{
		if(_playerDodge == DodgeState.DODGE_LEFT || _playerDodge == DodgeState.DODGE_RIGHT)
			return;

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
//		else if (PunchTarget.RIGHT_TOP == (PunchTarget)target)
//		{
//			anim.SetTrigger();
//		}
//		else if (PunchTarget.LEFT_TOP == (PunchTarget)target)
//		{
//			anim.SetTrigger();
//		}
//		else if (PunchTarget.RIGHT_LOW == (PunchTarget)target)
//		{
//			anim.SetTrigger();
//		}
//		else if (PunchTarget.LEFT_LOW == (PunchTarget)target)
//		{
//			anim.SetTrigger();
//		}
	}
}
