using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidDiedEvent : GameEvent {
	private readonly Kid kid;

	public Kid Kid { get { return kid; } }

	public KidDiedEvent(Kid kid) {
		this.kid = kid;
	}
}
