using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {

    [SerializeField] private float radius = 1.5f;
	public float Radius { get { return radius; } }

	private MeshRenderer mr;

	[SerializeField] private List<GameObject> discovered = new List<GameObject>();
	public List<GameObject> Discovered {
		get {
			discovered.RemoveAll(item => item == null);
			return discovered; 
		} 
	}

	void Awake () {
		mr = gameObject.GetComponentInChildren<MeshRenderer>();
	}

	void Start() {
        UpdateSize();
	}

	private void UpdateSize() {
		if (radius > 1.5) {
			gameObject.transform.localScale = new Vector3(radius, gameObject.transform.localScale.y, radius);
		}
	}

	public void UpgradeSize(float radiusStep) {
		radius += radiusStep;
		UpdateSize();
	}

	public void Show () {
		mr.enabled = true;
	}

	public void Hide () {
		mr.enabled = false;
	}

    void OnTriggerEnter(Collider collider) {
        discovered.Add(collider.gameObject);
    }

    void OnTriggerExit(Collider collider) {
        discovered.Remove(collider.gameObject);
    }
}
