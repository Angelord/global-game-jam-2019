using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

	public GameObject offerGUIPref;
	public List<ShopOffer> initialOffers = new List<ShopOffer>();
	public Transform optionGroup;
	public float selectorSpeed = 2000.0f;
	public float swithcFreq = 1.0f;
	public float enlargedSelectorSize = 130.0f;
	public Text candyText;

	public Text[] descriptions = new Text[2];
	public RectTransform[] selectors = new RectTransform[2];
	public int[] playerSelections = new int[2];
	public float[] lastSwitches = new float[2];
	private float defaultSelectorSize;

	// Use this for initialization
	void Start () {
		Progress.Candy += 100;

		defaultSelectorSize = selectors[0].sizeDelta.x;

		#if UNITY_EDITOR 
			if(Progress.Offers.Count == 0) {
				foreach(var offer in initialOffers) {
					Progress.AddOffer(offer);
				}
			}
		#endif

		if(Progress.Day == 2) {
			foreach(var offer in initialOffers) {
				Progress.AddOffer(offer);
			}
		}

		foreach(var offer in Progress.Offers) {
			GameObject offerGUi = Instantiate(offerGUIPref, optionGroup);
			offerGUIPref.GetComponent<ShopOfferGUI>().Initialize(offer);
		}

		for(int i = 0; i < 2; i++) {
			// descriptions[i].Color = Kid.PLAYER_COLORS[i];
			descriptions[i].transform.parent.GetComponent<Graphic>().color = Kid.PLAYER_COLORS[i];
			selectors[i].GetComponent<Image>().color = Kid.PLAYER_COLORS[i];
		}
	}

	private void Update() {

		for(int i = 0; i < 2; i++) {
			if(playerSelections[i] >= Progress.Offers.Count) {
				playerSelections[i] = Progress.Offers.Count - 1;
			}

			if((Time.time - lastSwitches[i]) <= swithcFreq) {
				continue;
			}

			float hor = Input.GetAxis("Horizontal_" + i);
			if(hor >= 0.1f) {
				playerSelections[i] = (playerSelections[i] + 1) % Progress.Offers.Count;
				lastSwitches[i] = Time.time;
			}
			 
			if(hor <= -0.1f) {
				playerSelections[i] = playerSelections[i] - 1;
				if(playerSelections[i] < 0) { playerSelections[i] = Progress.Offers.Count - 1; } 
				lastSwitches[i] = Time.time;
			}

			descriptions[i].text = Progress.Offers[playerSelections[i]].description;

			if(i == 1 && playerSelections[0] == playerSelections[1]) {
				selectors[i].sizeDelta = new Vector2(enlargedSelectorSize, enlargedSelectorSize);
			}
			else {
				selectors[i].sizeDelta = new Vector2(defaultSelectorSize, defaultSelectorSize);
			}
		}

		for(int i = 0; i < 2; i++) {
			Vector2 targetPos = optionGroup.GetChild(playerSelections[i]).GetComponent<RectTransform>().position;
			selectors[i].anchoredPosition = Vector3.Lerp(selectors[i].anchoredPosition, targetPos, selectorSpeed * Time.deltaTime); 
		}

		for(int i = 0; i < 2; i++) {
			if(Input.GetButtonDown("Attack_" + i)) {
				optionGroup.GetChild(playerSelections[i]).GetComponent<ShopOfferGUI>().Buy();
			}
		}

		candyText.text = "$ " + Progress.Candy.ToString();
	}

}
