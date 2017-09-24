using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    public float minRange = 3;
	public float maxRange = 5;
    float diffRange;

	public float minIntensity = 5;
	public float maxIntensity = 10;
    float diffIntensity;

    public float intensitySpeed = 1;
    public float rangeSpeed = 1;

    public float movementAllowance = 1.0f;
	public float movementSpeed = 1.0f;


	private Light theLight;

    private float timeCounter;
    private float rand1, rand2, rand3, rand4;

    private float strength = 1.0f;



    Vector3 originalPosition;

    public void SetGeneralStrengh(float value){
        strength = Mathf.Clamp(value, 0.0f, 1.0f);
    }

    private void Awake()
    {
        theLight = GetComponent<Light>();
    }

    private void Start(){
        timeCounter = 0.0f;
        rand1 = Random.value;
		rand2 = Random.value;
        rand3 = Random.value;
		rand4 = Random.value;
        originalPosition = theLight.transform.localPosition;
	}
	
	void FixedUpdate () {
        
		diffRange = maxRange - minRange;
		diffIntensity = maxIntensity - minIntensity;
        
        timeCounter += Time.fixedDeltaTime;
        theLight.range = strength * (minRange + diffRange * (Mathf.PerlinNoise(timeCounter * rangeSpeed,  rand1)));
        theLight.intensity = strength * (minIntensity + diffIntensity * Mathf.PerlinNoise(timeCounter * intensitySpeed, rand2));

        Vector3 newPosition = originalPosition;
        newPosition.x += movementAllowance * strength * (((Mathf.PerlinNoise(timeCounter * movementSpeed + rand4 , timeCounter * movementSpeed + rand3) * 2.0f) - 1.0f));
        newPosition.y += movementAllowance * strength * (((Mathf.PerlinNoise(timeCounter * movementSpeed + rand4, timeCounter * movementSpeed + rand4) * 2.0f) - 1.0f));

        theLight.transform.localPosition = newPosition;
	}


}
