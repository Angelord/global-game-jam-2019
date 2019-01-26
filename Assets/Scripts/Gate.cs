using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

	public void Open() {
		
		this.GetComponentInChildren<Collider>().enabled = false;
		this.GetComponentInChildren<Animator>().SetTrigger("Open");
	}
}
