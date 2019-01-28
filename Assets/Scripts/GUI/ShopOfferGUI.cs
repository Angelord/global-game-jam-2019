using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOfferGUI : MonoBehaviour {

	[SerializeField] private ShopOffer offer;
	[SerializeField] private Text priceText;
	[SerializeField] private Image icon;

	private Text descriptionText;

	public ShopOffer Offer { get { return offer; } }

	public void Initialize(ShopOffer offer, Text descriptionText = null) {
		this.offer = offer;
		this.descriptionText = descriptionText;
		UpdateDisplay();
	}

	public void OnHover() {
		if(descriptionText != null) {
			descriptionText.text = offer.description;
		}
 	}
	
	public void OnHoverEnd() {
		if(descriptionText != null) {
			descriptionText.text = "";
		}
	}
	

	public void Buy() {

		if(!Progress.CanAfford(offer)) {
			AudioController.Instance.PlaySound("not_enough_money");
			return;
		}

		AudioController.Instance.PlaySound("cashier");

		Progress.Candy -= offer.price;

		switch(offer.offerType) {
			case OfferType.BuyBanana:
			Progress.ModAmmo(UsableType.Banana, 1);
			break;
			case OfferType.BuyDummy:
			Progress.ModAmmo(UsableType.Dummy, 1);
			break;
			case OfferType.BalloonUpgrade:
			Progress.UpgradeUsable(UsableType.Balloon);
			break;
			case OfferType.HouseUpgrade:
			Progress.UpgradeHouse();
			break;
		}

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
			UpdateDisplay();
		}
	}

	private void UpdateDisplay() {
		priceText.text = offer.price.ToString();
		icon.sprite = offer.icon;
	}
}
