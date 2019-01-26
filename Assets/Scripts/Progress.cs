using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Progress {

	private static bool started = false;
	private static int day = 0;

	public static bool Started { get { return started; } set { started = value; } }
	public static int Day { get { return day; } }

	public static void Reset() {
		day = 0;
		//...
	}

	public static void DayPassed() {
		day++;
	}
}

