using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlScript : MonoBehaviour {

	public Hv_testing_AudioLib HeavyScript;
	public MainSquareBehaviour behaviour;
	float lfo_rate;

	// Use this for initialization
	void Start () {
		lfo_rate = HeavyScript.GetFloatParameter (Hv_testing_AudioLib.Parameter.Lfo_rate);
		behaviour = GameObject.Find ("Main Camera").GetComponent<MainSquareBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
		lfo_rate = Remap (behaviour.rotationSpeed, 0, 100, 0, 15);
		HeavyScript.SetFloatParameter (Hv_testing_AudioLib.Parameter.Lfo_rate, lfo_rate);
	}

	public static float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}
