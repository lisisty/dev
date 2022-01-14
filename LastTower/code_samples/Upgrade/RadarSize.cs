using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSize : MonoBehaviour, IUpgradable {

	[SerializeField] private UpgradeToolInfo toolInfo;
	public UpgradeToolInfo ToolInfo { get { return toolInfo; } }

	private Radar radar;

	void Start() {
		radar = gameObject.transform.parent.GetComponentInChildren<Radar>();
	}

	public void Upgrade() {
		radar.UpgradeSize(toolInfo.UpgradeStep);
	}

	public float CurrentValue() {
		return radar.Radius;
	}
}
