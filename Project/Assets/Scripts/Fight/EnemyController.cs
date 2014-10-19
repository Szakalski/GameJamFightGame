using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	private float enemyStamina = 100f;
	private Slider _enemyStaminaSlider;

	private FightBase _fightBase;

	private void Awake()
	{
		_fightBase = GameObject.Find("FightController").GetComponent<FightBase>();
		_enemyStaminaSlider = GameObject.Find ("EnemyStaminaBar").GetComponent<Slider>();
	}
	
	private void Start ()
	{
		InitiateListeners(_fightBase.eventManager);
	}

	private void InitiateListeners(EventManager eventManager)
	{
		eventManager.addEventListener(FightBase.E_SEND_DAMAGE, gameObject, "HealthUpdate");
	}

	private void HealthUpdate(CustomEvent e)
	{
		float damage = (float)e.arguments["damage"];
		enemyStamina -= damage;

		_enemyStaminaSlider.value = enemyStamina / 100f;

		#if UNITY_EDITOR
			Debug.Log("Damage done to enemy: " + damage);
		#endif

		if(enemyStamina <= 0)
			_fightBase.eventManager.dispatchEvent(new CustomEvent(FightBase.E_PLAYER_WON));
	 }
}
