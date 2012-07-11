using UnityEngine;
using System.Collections;

public static class ControlData {

	public static Man man;
	public static Control control;
	
	
	public static void init(Man m, Control c) {
		man = m;
		control = c;
	}
}
