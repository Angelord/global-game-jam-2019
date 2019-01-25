using System.Collections.Generic;
using UnityEngine; 

public enum Direction {
	Up,
	Down,
	Left,
	Right
}

public static class DirUtil {

	private static readonly Dictionary<Direction, Vector3> MAPPINGS = new Dictionary<Direction, Vector3>() {
		{ Direction.Up, new Vector3(0.0f, 0.0f, 1.0f) },
		{ Direction.Down, new Vector3(0.0f, 0.0f, -1.0f) },
		{ Direction.Left, new Vector3(-1.0f, 0.0f, 0.0f) },
		{ Direction.Right, new Vector3(1.0f, 0.0f, 0.0f) }

	};

	public static Vector3 DirectionToVec(Direction dir) {
		return MAPPINGS[dir];
	}
}