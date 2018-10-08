using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour {
    public int _band;
    public float _startScale, _scaleMultiplier;
    public bool _userBuffer;// use the buffer or not
    Material _material;

	// Use this for initialization
	void Start () {
        _material = GetComponent<MeshRenderer>().materials[0];
	}
	
	// Update is called once per frame
	void Update () {
        if (_userBuffer)
        {
            float f = (AudioPeer._bandBuffer[_band] * _scaleMultiplier) +_startScale;
            string fromFloatToString = f.ToString();
            Debug.Log(fromFloatToString);

            if (f >= 10f)
            {
                transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._bandBuffer[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
                // emission color of the shader on the material, we want to set the color on value between 0-1
                Color _color = new Color(AudioPeer._bandBuffer[_band], AudioPeer._bandBuffer[_band], AudioPeer._bandBuffer[_band]);
                _material.SetColor("EmissionColor", _color);
            }
        }
	}
}
