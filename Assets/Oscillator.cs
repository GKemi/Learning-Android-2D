using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {

	public double frequency = 110;
	private double increment;
	private double phase;
	private double sampling_frequency = 48000;


	MainSquareBehaviour squareBehaviour;
	private float multiplier;

	public float gain;

	void Start(){
		GameObject cameraObject = GameObject.Find ("Main Camera");
		squareBehaviour = cameraObject.GetComponent<MainSquareBehaviour>();
	}

	void FixedUpdate(){
		multiplier = squareBehaviour.rotationSpeed;
	}

	void OnAudioFilterRead(float[] data, int channels){
		increment = frequency * (multiplier/10) * Mathf.PI / sampling_frequency;


		for (int i = 0; i < data.Length; i += channels) {
			phase += increment;
			data [i] = (float)(gain * Mathf.Sin ((float)phase));

			if (channels == 2) {
				data [i + 1] = data [i];
			}

			if (phase > (Mathf.PI * 2)){
				phase = 0.0;
			}
		}


	}


}
