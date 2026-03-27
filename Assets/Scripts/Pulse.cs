using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UIElements;


public class Pulse : MonoBehaviour
{
    public float speed;

    public AnimationCurve heartbeatCurve;
    public float duration;
    public float progress;

    private Vector3 pulsePosition;
    public float pulseDuration = 0.5f;
    public float pulseProgress;
    public bool isPulse;

    private Coroutine pulseCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pulsePosition = transform.position;
        pulsePosition += Time.deltaTime * speed * transform.right;
        pulsePosition.z = 0f;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.x > Screen.width + 100)
        {
            pulsePosition.x = -10;
        }

        pulseProgress += Time.deltaTime / pulseDuration;

        if (pulseProgress > pulseDuration)
        {
            pulseCoroutine = StartCoroutine(PulseUpdate());

            pulseProgress = 0f;
        }

        transform.position = pulsePosition;
    }

    private IEnumerator PulseUpdate()
    {
        while (progress < duration)
        {
            progress += Time.deltaTime;

            pulsePosition.y = heartbeatCurve.Evaluate(progress / duration);

            if (progress > duration)
            {
                progress = 0f;
            }

            yield return null;
        }
    }
}
