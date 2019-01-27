using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : MonoBehaviour {

	[SerializeField] private UsableType usableType;
	[SerializeField] private Sprite icon;
	[SerializeField] private float cooldown;
	[SerializeField] private GameObject objectPref;
	[SerializeField] private Transform spawnPoint;
	private Kid kid;
	private float lastAttack;
	private bool canUse = true;

	public Sprite Icon { get { return icon; } } 
	public bool CanUse { get { return !Progress.IsFinite(usableType) || (Progress.GetAmmo(usableType) > 0); } }

	public void Use() {
		if((Time.time - lastAttack) >= cooldown) {
			GameObject newObject = Instantiate(objectPref, spawnPoint.position, Quaternion.identity);

			Balloon balloonComp = newObject.GetComponent<Balloon>();
			if(balloonComp != null) {
				newObject.GetComponent<Balloon>().Launch(kid.Direction);
			}

			if(Progress.IsFinite(usableType)) {
				Progress.ModAmmo(usableType, -1);
				Debug.Log(Progress.GetAmmo(usableType));
			}

			//TODO : Event
			lastAttack = Time.time;
		}
	}

	private void Start() {

		lastAttack = -cooldown;

		kid = GetComponentInParent<Kid>();
	}
}

public enum UsableType {
	Balloon,
	Banana,
	Count
}