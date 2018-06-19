using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySong : MonoBehaviour {
    
	public AudioSource audioSource;
    
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		Invoke("Play", 0.0f);
	}

	void Play() {
		audioSource.Play(); 
	}

	// Update is called once per frame
	void Update () {
		
	}
}
