using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	
	public float dayTime, nightTime, duskTime, dawnTime;
	float time, lastTransition;
	public Color nightlight, daylight, dusklight, dawnlight;
	Color light, lastLight;
	
	public Control c;
	
	private enum times {
		dawn,
		day,
		dusk,
		night
	};
	
	public enum WorldEvents {
		NightStarted	
	};
	
	times timeEnum;
	
	// Use this for initialization
	void Start () {
		
		timeEnum = times.night;
		light = nightlight;
		lastTransition = dawnTime;
		RenderSettings.ambientLight = light;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		
		switch(timeEnum) {
			case times.dawn:
				if (time > dayTime) {
					light = daylight;
					lastTransition = dayTime;
					timeEnum = times.day;
				} else {
					TransitionLight(daylight, dayTime);
				}
			break;
			case times.day:
				if (time > duskTime) {
					lastTransition = duskTime;
					timeEnum = times.dusk;
				}
			break;
			case times.dusk:
				if (time > nightTime) {
					light = nightlight;
					lastTransition = nightTime;
					timeEnum = times.night;
					c.WorldEvent(WorldEvents.NightStarted);
					time = 0;
				} else {
					TransitionLight(nightlight, nightTime);
				}
			break;
			case times.night:
				if (time > dawnTime) {
					lastTransition = dawnTime;
					timeEnum = times.dawn;
				}
			break;
		}
	}
	
	void TransitionLight(Color toColor, float toTime) {
		light = Color.Lerp(light, toColor,
			1f - (toTime - time) / (toTime - lastTransition));
		Debug.Log(1f - (toTime - time) / (toTime - lastTransition));
		RenderSettings.ambientLight = light;
	}
}
