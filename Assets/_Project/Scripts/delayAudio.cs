using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delayAudio : MonoBehaviour {
    AudioSource myAudio;

	// Use this for initialization
	void Start () {
        myAudio = GetComponent<AudioSource>();
        Invoke("PlayAudio", 11.2f);
	}

    void PlayAudio() {
        myAudio.Play();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
