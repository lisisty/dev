using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSpeed : MonoBehaviour, IUpgradable {

	[SerializeField] private UpgradeToolInfo toolInfo;
	public UpgradeToolInfo ToolInfo { get { return toolInfo; } }

	private Aim aim;

	void Start() {
		aim = gameObject.GetComponentInParent<Aim>();
	}

	public void Upgrade () {
		aim.UpgradeSpeed(toolInfo.UpgradeStep);
	}

	public float CurrentValue () {
		return aim.Speed;
	}
}
