using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

	public void Open() {

		//TODO : playe open animation
		
		this.GetComponent<Collider>().enabled = false;
	}
}
