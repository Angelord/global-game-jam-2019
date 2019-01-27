using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour {

	private List<Unit> inRange = new List<Unit>();

	public bool InRange(Unit unit) {
		return inRange.Contains(unit);
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "Treehouse") {
			Unit otherUnit = other.gameObject.GetComponent<Unit>();
			if(otherUnit != null && !inRange.Contains(otherUnit)) {
				inRange.Add(otherUnit);
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		Unit otherUnit = other.gameObject.GetComponent<Unit>();
		if(otherUnit != null && inRange.Contains(otherUnit)) {
			inRange.Remove(otherUnit);
		}
	}
}
