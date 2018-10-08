using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateNotes : MonoBehaviour {
    const int NUM_BANDS = 5;
    public float _startScale, _scaleMultiplier;

    public GameObject greenNote;
    public GameObject redNote;
    public GameObject yellowNote;
    public GameObject blueNote;
    public GameObject orangeNote;

    private static GameObject[] noteObjects = new GameObject[5];
    private static bool[] notesOn = { true, true, true, true, true };

    // Use this for initialization
    void Start () {
        noteObjects[0] = greenNote;
        noteObjects[1] = redNote;
        noteObjects[2] = yellowNote;
        noteObjects[3] = blueNote;
        noteObjects[4] = orangeNote;
    }
	
	// Update is called once per frame
	void Update () {
        for(int _band = 0; _band < NUM_BANDS; ++_band) {
            float f = (AudioPeer._bandBuffer[_band] * _scaleMultiplier) + _startScale;
            string fromFloatToString = f.ToString();

            if (f >= 10f && notesOn[_band])
            {
                // transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._bandBuffer[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);

                GameObject noteObj = noteObjects[_band];
                notesOn[_band] = false;

                Vector3 location = new Vector3(noteObj.transform.position.x, noteObj.transform.position.y, 0.5f);
                GameObject noteObject = Instantiate(noteObj, location, Quaternion.identity, this.transform);
                noteObject.transform.localPosition = location;
                noteObject.transform.localRotation = noteObj.transform.localRotation;
            }
            else if (f < 10f) {
                notesOn[_band] = true;
            }
        }

        
    }
}