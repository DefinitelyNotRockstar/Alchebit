using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffect : MonoBehaviour {


	public float duration = 10.0f;

	private float remainingTime;
	public float fadeOutThreshold = 0.5f;
	private LightFlicker lightFlicker;

	private void Start(){
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
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
            GetComponent<CircleCollider2D>().enabled = false;
			foreach (Collider2D hit in colliders)
			{
				if (hit.gameObject.CompareTag("Enemy"))
				{
                    hit.gameObject.GetComponent<EnemyController>().EndSlow();
				}
			}

			lightFlicker.SetGeneralStrengh(remainingTime / fadeOutThreshold);
		}
    }


    private void OnTriggerStay2D(Collider2D _collider) {
        if (_collider.gameObject.CompareTag("Enemy")) {
            _collider.gameObject.GetComponent<EnemyController>().Slow();
        }
    }

    private void OnTriggerExit2D(Collider2D _collider) {
		if (_collider.gameObject.CompareTag("Enemy")) {
			_collider.gameObject.GetComponent<EnemyController>().EndSlow();
		}
    }
}
