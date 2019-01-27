using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Shop Offer")]
public class ShopOffer : ScriptableObject {

	public int price;
	public OfferType offerType;
	public string name;
	[TextArea]
	public string description;
	public Sprite icon;
	public bool singlePurchase;
	public ShopOffer nextLevel = null;
	// protected abstract void Buy
}

public enum OfferType {
	BuyBanana,
	BuyDummy,
	BalloonUpgrade,
	HouseUpgrade,
}
