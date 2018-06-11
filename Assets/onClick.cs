using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClick : MonoBehaviour {

    public string key;
    private ParticleSystem[] particles;

	// Use this for initialization
	void Start ()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(this.key))
            foreach(ParticleSystem ps in particles) ps.Play();
    }
}
