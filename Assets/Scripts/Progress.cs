﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Progress {

	private static bool started = false;
	private static int day = 0;
	private static int candy = 0;
	private static List<ShopOffer> offers = new List<ShopOffer>();
	private static int[] usableAmmo = new int[(int)UsableType.Count];

	public static bool GameStarted { get { return started; } set { started = value; } }
	public static int Day { get { return day; } }
	public static int Candy { get { return candy; } set { candy = value; } }
	public static List<ShopOffer> Offers { get { return offers; } }

	public static bool IsFinite(UsableType usable) {
		if(usable == UsableType.Balloon) { return false; }
		return true;
	}

	public static void ModAmmo(UsableType usable, int amount) {
		usableAmmo[(int)usable] += amount;
	}

	public static int GetAmmo(UsableType usable) {
		return usableAmmo[(int)usable];
	}

	public static bool CanAfford(ShopOffer offer) {
		return Candy >= offer.price;
	}

	public static void AddOffer(ShopOffer offer) {
		offers.Add(offer);
	}

	public static void RemoveOffer(ShopOffer offer) {
		offers.Remove(offer);
	}

	public static void Reset() {
		day = 0;
		candy = 0;
		offers.Clear();
	}

	public static void DayPassed() {
		day++;
	}
}

