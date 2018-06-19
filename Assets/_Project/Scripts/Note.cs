using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

	// Use this for initialization
	void Start () {}
    
	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.back * 5.0f * Time.deltaTime);
	}
}
