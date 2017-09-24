using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour {

	public float duration = 0.2f;

	private float remainingTime;
	public float fadeOutThreshold = 0.2f;
	private LightFlicker lightFlicker;

	private void Start()
	{
		Destroy(gameObject, duration);
		remainingTime = duration;
	}

	private void Awake()
	{
		lightFlicker = GetComponentInChildren<LightFlicker>();
	}

	private void FixedUpdate()
	{
		remainingTime -= Time.fixedDeltaTime;
		if (remainingTime < fadeOutThreshold)
        {
			lightFlicker.SetGeneralStrengh(remainingTime / fadeOutThreshold);
		}
	}
}
