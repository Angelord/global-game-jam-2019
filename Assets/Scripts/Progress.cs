using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Progress {

	private static int day = 0;

	public static int Day { get { return day; } }

	public static void DayPassed() {
		day++;
	}
}

