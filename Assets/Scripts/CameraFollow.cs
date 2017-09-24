using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float followSpeed = 0.05f;
    public float magnitudeLimit = 350f;
    public float screenLimit = 100f;
    public float transformScale = 3f;


    //Camera myCamera;


    // Use this for initialization
    void Start () {

        //myCamera = GetComponent<Camera>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (target) {

            Vector2 diffTransform = target.position - transform.position;


			//Debug.Log(diffTransform.magnitude);

			if (diffTransform.magnitude > screenLimit)
            {
                float diffScale = (diffTransform.magnitude * transformScale) / (magnitudeLimit);

                transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * diffScale);

                Vector3 temp = transform.position;

                temp.z = -10;

                transform.position = temp;
            
            }
                
        }


	}
}
