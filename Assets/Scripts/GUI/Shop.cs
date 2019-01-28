using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {

	public GameObject offerGUIPref;
	public List<ShopOffer> initialOffers = new List<ShopOffer>();
	public Transform optionGroup;
	public float selectorSpeed = 2000.0f;
	public float swithcFreq = 1.0f;
	public float enlargedSelectorSize = 130.0f;
	public Text candyText;
	public Text description;


	public void Continue() {
		ScreenEffects.FadeOut(1.5f);
		Invoke("DoContinue", 1.5f);
	}

	private void DoContinue() {
		SceneManager.LoadScene("Treehouse");
	}
	
	private void Start () {
		ScreenEffects.FadeIn(2.0f);

		AudioController.Instance.PlaySound("rooster");

		AudioController.Instance.SetLoop("store");
        AudioController.Instance.SetLoopVolume(0.0f);
        AudioController.Instance.FadeInLoop(0.12f, 0.45f);

		if(Progress.Offers.Count == 0) {
			foreach(var offer in initialOffers) {
				Progress.AddOffer(offer);
			}
		}

		foreach(var offer in Progress.Offers) {
			GameObject offerGUi = Instantiate(offerGUIPref, optionGroup);
			offerGUi.GetComponent<ShopOfferGUI>().Initialize(offer, description);
		}

	}

	private void Update() {

		candyText.text = Progress.Candy.ToString();
	}

}
