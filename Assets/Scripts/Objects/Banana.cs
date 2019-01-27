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

				GetComponent<Collider>().enabled = false;

				GetComponentInChildren<Animator>().SetTrigger("Splash");
				
				Invoke("DestroySelf", 0.7f);
			}
		}
	}

	private void DestroySelf() {
		Destroy(this.gameObject);
	}
}
