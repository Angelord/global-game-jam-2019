using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Progress {

	public const int LAST_DAY = 5;
	private static bool started = false;
	private static ProgressData savedData = new ProgressData();
	private static ProgressData data = new ProgressData();
	
	public static bool GameStarted { get { return started; } set { started = value; } }
	public static int Day { get { return data.day; } set { data.day = value; } }
	public static int Candy { get { return data.candy; } set { data.candy = value; } }
	public static List<ShopOffer> Offers { get { return data.offers; } }
	public static int HouseLevel { get { return data.houseLevel; } }

	public static void Save() {
		if(savedData.day != data.day) {
			data.CopyTo(savedData);
		}
	}

	public static void Load() {
		savedData.CopyTo(data);
	}

	public static void UpgradeHouse() {
		data.houseLevel++;
	}

	public static void Reset() {
		data = new ProgressData();
		Progress.ModAmmo(UsableType.Dummy, +1);
		Progress.ModAmmo(UsableType.Banana, +3);

		savedData = new ProgressData();
		savedData.day = -1;
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

public class ProgressData {
	public int day = 0;
	public int candy = 0;
	public int houseLevel = 0;
	public List<ShopOffer> offers = new List<ShopOffer>();
	public int[] usableAmmo = new int[(int)UsableType.Count];
	public int[] usableLevels = new int[(int)UsableType.Count];

	public void CopyTo(ProgressData target) {
		target.day = day;
		target.candy = candy;
		target.houseLevel = houseLevel;
		target.offers.Clear();
		foreach(var offer in offers) {
			target.offers.Add(offer);
		}
		for(int i = 0; i < (int)UsableType.Count; i++) {
			target.usableAmmo[i] = usableAmmo[i];
			target.usableLevels[i] = usableLevels[i];
		}
	}
}

