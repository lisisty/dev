using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradable {

	UpgradeToolInfo ToolInfo { get; }

	void Upgrade();
	float CurrentValue();

}
