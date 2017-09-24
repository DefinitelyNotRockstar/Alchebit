using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float followSpeed = 0.05f;
    public float magnitudeLimit = 350f;
    public float screenLimit = 100f;
    public float transformScale = 3f;


    //Camera myCamera;

    public float mapX = 14.0f;
    public float mapY = 11.0f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;


    // Use this for initialization
    void Start () {


     


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

    private void LateUpdate()
    {

		float vertExtent = Camera.main.orthographicSize;
		float horzExtent = vertExtent * Screen.width / Screen.height;

		minX = horzExtent - mapX / 2.0f;
		maxX = mapX / 2.0f - horzExtent;
		minY = vertExtent - mapY / 2.0f;
		maxY = mapY / 2.0f - vertExtent;
		
			Vector3 v3 = transform.position;
			v3.x = Mathf.Clamp(v3.x, minX, maxX);
			v3.y = Mathf.Clamp(v3.y, minY, maxY);
			transform.position = v3;
		
    }

}
