using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

	[SerializeField] private Animator fence; 

	public void Open() {
		
		this.GetComponentInChildren<Collider>().enabled = false;
		this.GetComponentInChildren<Animator>().SetTrigger("Open");

		fence.SetTrigger("Light");
	}
}
