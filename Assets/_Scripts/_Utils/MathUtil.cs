using UnityEngine;
using System.Collections;

public static class MathUtil {

	public static float CrowFliesDistance(Vector3 vector) {
		return new Vector3(vector.x, 0, vector.z).magnitude;	
	}	
}
