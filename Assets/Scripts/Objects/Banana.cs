using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Insta-kills an enemy that steps on it
public class Banana : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy") {
			Unit unitComp = other.gameObject.GetComponent<Unit>();
			if(unitComp != null) {
				unitComp.TakeDamage(unitComp.CurHealth);
				Destroy(this.gameObject);
			}
		}
	}
}
