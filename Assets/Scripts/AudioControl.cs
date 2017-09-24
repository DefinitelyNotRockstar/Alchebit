using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioControl {

	private static float time;

	public static float Time {
		get {
			return time;
		}
		set {
			time = value;
		}
	}
}
