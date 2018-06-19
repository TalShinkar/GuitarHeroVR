using UnityEngine;
using System.Collections;

public class fire : MonoBehaviour
{
    ParticleSystem targetFire;
    float timeLeft = 0;
    int currentColor = 0;

    void Start()
    {
        timeLeft = 5.0f;
        targetFire = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (timeLeft <= Time.deltaTime)
        {
            targetFire.Play();
            timeLeft = 5.0f;
        }
        else
            timeLeft -= Time.deltaTime;
    }
}