using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidSpawnedEvent : GameEvent {

	private readonly Kid kid;

	public Kid Kid { get { return kid; } }

	public KidSpawnedEvent(Kid kid) {
		this.kid = kid;
	} 
}
