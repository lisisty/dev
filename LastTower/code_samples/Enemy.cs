using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public static Action<Enemy> OnDead;

	public static List<Enemy> LivingEnemies { get { return livingEnemies; } }
	private static List<Enemy> livingEnemies = new List<Enemy>();
	
	public EnemyInfo EnemyInfo { get { return enemyInfo; } }
    public float CurrentHP { get { return currentHP; } }

    [SerializeField] private EnemyInfo enemyInfo;

    private float currentHP;
    private HpBar hpBar;

	public void GiveSoul (EnemyInfo info) {
		enemyInfo = info;
		hpBar = gameObject.GetComponentInChildren<HpBar>();
		GiveHP(enemyInfo.Hp);
		var legs = GetComponent<AIMove>();
		legs.SetSpeed(enemyInfo.Speed);
		legs.SetDestination(FindObjectOfType<Tower>().gameObject.transform.position); // dontforgetfixthisshit
		livingEnemies.Add(this);
	}

    public void TakeHP (float damage) {
		currentHP -= damage;
		if (currentHP <= 0) {
			Dead();
			return;
		}
		hpBar.UpdateHpBar(currentHP, enemyInfo.Hp);
	}

    public void GiveHP (float hp) {
		currentHP += hp;
		if (currentHP > enemyInfo.Hp) {
			currentHP = enemyInfo.Hp;
		}
		hpBar.UpdateHpBar(currentHP, enemyInfo.Hp);
    }

	public void Kill () {
		TakeHP(currentHP);
	}

	private void Dead () {
		livingEnemies.Remove(this);
		OnDead(this);
		Destroy(gameObject);
	}

	void OnDestroy () {
		print("on destroy " + gameObject.name);
	}
}
