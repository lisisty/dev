using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpdateCoinsQuantity : MonoBehaviour {

	[SerializeField] private Text txtCoinsDown;
	[SerializeField] private Text txtCoinsUp;
	[SerializeField] private Text txtCoins;

	void Start () {
		Coins.OnChange += UpdateValueInUi;
	}

	private void UpdateValueInUi (int newCoins) {
		if (Coins.Current < newCoins) {
			txtCoinsUp.DOFade(0, 0f).SetUpdate(true);
			txtCoinsUp.gameObject.SetActive(true);
			txtCoinsUp.DOFade(1, 0.5f).OnComplete(() => txtCoinsUp.gameObject.SetActive(false)).SetUpdate(true);
		} else {
			txtCoinsDown.DOFade(0, 0f).SetUpdate(true);
			txtCoinsDown.gameObject.SetActive(true);
			txtCoinsDown.DOFade(1, 0.5f).OnComplete(()=>txtCoinsDown.gameObject.SetActive(false)).SetUpdate(true);
		}
		txtCoins.text = newCoins.ToString();
	}
}
