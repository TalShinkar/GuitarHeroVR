using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour {
    static int NUM_SAMPLES = 1024;
    public float _maxScale;

    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCube = new GameObject[NUM_SAMPLES]; // array of the instantiate game objects
	// Use this for initialization
	void Start () {
        for (int i = 0; i < NUM_SAMPLES; i++) {
            GameObject _instanceSampleCube = (GameObject)Instantiate(_sampleCubePrefab);
            _instanceSampleCube.transform.position = this.transform.position;
            _instanceSampleCube.transform.parent = this.transform;
            _instanceSampleCube.name = "SampleCube" + i;
            // we have 512 cubes and circle is 360 so we move each time by 0.703125
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            _instanceSampleCube.transform.position = Vector3.forward * 100;
            _sampleCube[i] = _instanceSampleCube;
        }
	}

    // Update is called once per frame 
    void Update()
    {
        for (int i = 0; i < NUM_SAMPLES; i++)
        {
            if (_sampleCube != null) {
                _sampleCube[i].transform.localScale = new Vector3(10, (AudioPeer._samples[i] * _maxScale) + 2, 10);
            }
        }
    }
}
