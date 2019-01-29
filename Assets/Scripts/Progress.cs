using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Progress {

	public const int LAST_DAY = 5;
	private static bool started = false;
	private static ProgressData savedData = new ProgressData();
	private static ProgressData data = new ProgressData();
	private static int daySaved = -1;
	
	public static bool GameStarted { get { return started; } set { started = value; } }
	public static int Day { get { return data.day; } set { data.day = value; } }
	public static int Candy { get { return data.candy; } set { data.candy = value; } }
	public static List<ShopOffer> Offers { get { return data.offers; } }
	public static int HouseLevel { get { return data.houseLevel; } }

	public static void Save() {
		if(daySaved != data.day) {
			savedData = data;
			daySaved = data.day;
		}
	}

	public static void Load() {
		data = savedData;
	}

	public static void UpgradeHouse() {
		data.houseLevel++;
	}

	public static void Reset() {
		data = new ProgressData((int)UsableType.Count);
	}

	public static void UpgradeUsable(UsableType type) {
		data.usableLevels[(int)type]++;
	}

	public static int GetUsableLevel(UsableType type) {
		return data.usableLevels[(int)type];
	} 

	public static bool IsFinite(UsableType usable) {
		if(usable == UsableType.Balloon) { return false; }
		return true;
	}

	public static void ModAmmo(UsableType usable, int amount) {
		data.usableAmmo[(int)usable] += amount;
	}

	public static int GetAmmo(UsableType usable) {
		return data.usableAmmo[(int)usable];
	}

	public static bool CanAfford(ShopOffer offer) {
		return Candy >= offer.price;
	}

	public static void AddOffer(ShopOffer offer) {
		data.offers.Add(offer);
	}

	public static void RemoveOffer(ShopOffer offer) {
		data.offers.Remove(offer);
	}

	public static void DayPassed() {
		data.day++;
	}
}

public struct ProgressData {
	public int day;
	public int candy;
	public int houseLevel;
	public List<ShopOffer> offers;
	public int[] usableAmmo;
	public int[] usableLevels;

	public ProgressData(int numUsables) {
		day = 0;
		candy = 0;
		houseLevel = 0;
		offers = new List<ShopOffer>();
		usableAmmo = new int[numUsables];
		usableLevels = new int[numUsables];
	}


	public void Print() {
		Debug.Log("printing");
	}
}

