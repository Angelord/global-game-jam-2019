using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveData")]
public class WaveData : ScriptableObject {

	[SerializeField] private Day[] days;

	public List<Wave> GetWavesForDay(int day) {
		if(day >= days.Length) {
			return days[days.Length - 1].waves;
		}

		return days[day].waves;
	}
	
	[System.Serializable]
	private class Day {
		
		public List<Wave> waves = new List<Wave>();		
	}
}
