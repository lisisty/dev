using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	public static List<GameObject> Enemies {get { return enemies; } }
	private static List<GameObject> enemies = new List<GameObject>();

	void Start () {
		LevelController.OnLevelDispose += OnLevelDispose;
		LevelController.OnLevelLoad += OnLevelLoad;
	}

	private void OnLevelLoad () {

	}

	private void OnLevelDispose () {
		StopAllCoroutines();
		foreach (var enemy in enemies) {
			Destroy(enemy);
		}
		enemies.Clear();
	}

	public void StartSpawn(EnemyInfo enemyInfo, int quanity, float delay) {
        StartCoroutine(_StartSpawn(enemyInfo, quanity, delay));
    }

    public IEnumerator _StartSpawn(EnemyInfo enemyInfo, int quanity, float delay) {
        for (int i = 0; i < quanity; i++) {
			BuildEnemy(enemyInfo);
            yield return new WaitForSeconds(delay);
        }
    }

    private void BuildEnemy (EnemyInfo enemyInfo) {
		GameObject enemyObj = Instantiate(enemyInfo.Prefab, gameObject.transform);
		enemies.Add(enemyObj);
		Enemy enemy = enemyObj.AddComponent<Enemy>();
        enemy.GiveSoul(enemyInfo);
	}
}
