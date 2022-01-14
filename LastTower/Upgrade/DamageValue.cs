using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageValue : MonoBehaviour, IUpgradable {

	[SerializeField] private UpgradeToolInfo toolInfo;
	public UpgradeToolInfo ToolInfo { get { return toolInfo; } }

	private IFireunit[] fireunits;

	void Start () {
		fireunits = gameObject.transform.parent.GetComponentsInChildren<IFireunit>();
	}

	public void Upgrade () {
		foreach (var fireunit in fireunits) {
			fireunit.UpgradeDamage(toolInfo.UpgradeStep);
		}
	}

	public float CurrentValue () {
		return fireunits[0].Damage;
	}
}