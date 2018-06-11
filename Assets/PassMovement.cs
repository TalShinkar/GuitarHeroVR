using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassMovement : MonoBehaviour {

    private Vector3 StartPosition = new Vector3(0.0f, 0.6f, 0.5f);
    private Vector3 EndPosition = new Vector3(0.0f, 0.6f, - 0.5f);

	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(- Vector3.forward * Time.deltaTime * 2.5f);
        if (this.transform.localPosition.z < this.EndPosition.z)
            this.transform.localPosition = this.StartPosition;
    }
}
