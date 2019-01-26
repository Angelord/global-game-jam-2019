using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOfferGUI : MonoBehaviour {

	[SerializeField] private ShopOffer offer;
	[SerializeField] private Text priceText;
	[SerializeField] private Image icon;

	public void Initialize(ShopOffer offer) {
		this.offer = offer;
		UpdateDisplay();
	}

	public void Buy() {

		//TODO : Show effect

		if(!offer.singlePurchase) {
			return;
		}

		Progress.RemoveOffer(offer);
		if(offer.nextLevel == null) {
			Destroy(this.gameObject);
		}
		else {
			Progress.AddOffer(offer.nextLevel);
			offer = offer.nextLevel;
		}
	}

	private void UpdateDisplay() {
		priceText.text = offer.price.ToString();
		icon.sprite = offer.icon;
	}
}
