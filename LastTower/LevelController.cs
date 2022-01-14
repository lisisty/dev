using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public struct LevelInfo {
	public int Num;
	public int coinsEarned;
}

public class LevelController : MonoBehaviour {

	[SerializeField] private Zone currentZone;
	[SerializeField] private Spawn spawn;

	public static Action OnLevelLoad;
	public static Action OnLevelDispose;
	public static Action OnLevelRun;
	public static Action<int> OnLevelWin;
	public static Action OnLevelLose;
	// public static Action OnLevelStop;

	private Level currentLevel;
	private bool wavesEnd = false;
	private bool enemiesRan = false;

	// private IEnumerator runEnemies;

	private void Win () {
		print("Win");
		OnLevelWin(currentLevel.Number);
		DisposeLevel();
	}

	private void Lose () {
		print("lose");
		OnLevelLose();
		DisposeLevel();
	}

	public void RestartLevel () {
		LoadLevel(currentLevel.Number);
	}

    public void LoadLevel (int number) {
		DisposeLevel();
		print("Load Level " + number);
		currentLevel = currentZone.Levels[number];

		GameUI.PlayMenu.OnStartTap += RunEnemies;
		GameUI.StartTimer.OnTimerEnd += RunEnemies;
		Tower.OnDestroed += Lose;

		OnLevelLoad();
    }


	private void DisposeLevel () {
		print("Level Dispose");
		StopAllCoroutines();
		
		GameUI.PlayMenu.OnStartTap -= RunEnemies;
		GameUI.StartTimer.OnTimerEnd -= RunEnemies;
		Tower.OnDestroed -= Lose;
		enemiesRan = false;
		wavesEnd = false;
		OnLevelDispose();
	}

	private void RunEnemies () {
		if (enemiesRan) return;

		enemiesRan = true;
		StartCoroutine(_RunEnemies());
		OnLevelRun();
	}

	// public void CheckWin () {
		// print("check win >>> waves end-" + wavesEnd + " | living enemies:" + spawn.LivingEnemies.Count);
		// if (wavesEnd && spawn.LivingEnemies.Count == 0) {
		// 	Win();
		// }
	// }


    private IEnumerator _RunEnemies () {
		foreach (var currentWave in currentLevel.Waves) {
			yield return StartCoroutine(spawn._StartSpawn(
				currentWave.Enemy, 
				currentWave.EnemiesQuantity, 
				currentWave.EnemySpawnDelay
			));
			yield return new WaitForSeconds(currentWave.AfterWaveDelay);
		}
		wavesEnd = true;
	}
}