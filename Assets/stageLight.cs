using UnityEngine;
using System.Collections;

public class stageLight : MonoBehaviour {
	public Color firstColor;
	public Color secondColor;

	Light targetLight;
	float timeLeft = 0;
	int currentColor = 0;

	Vector3 angle;
	float rotation = 0f;
	public float speed = 70.0f;
	public bool direction = true;

	void Start () {
		targetLight = GetComponent<Light> ();
		targetLight.color = firstColor;

		angle = transform.localEulerAngles;
	}

	void Update () {
		targetLight.transform.localEulerAngles = new Vector3(angle.x, Rotation(), angle.z);

		if (timeLeft <= Time.deltaTime) {
			if (currentColor == 0)
				targetLight.color = firstColor;
			else
				targetLight.color = secondColor;
			currentColor = (currentColor + 1) % 2;
			timeLeft = 2.0f;
		} else {
			Color targetColor = currentColor == 0 ? firstColor : secondColor;
			timeLeft -= Time.deltaTime;
			targetLight.color = Color.Lerp(targetLight.color, targetColor, Time.deltaTime / timeLeft);
		}
	}

	float Rotation()
	{
		rotation += speed * Time.deltaTime;
		if (rotation >= 360f) 
			rotation -= 360f; // this will keep it to a value of 0 to 359.99...
		return direction ? rotation : -rotation;
	}
}