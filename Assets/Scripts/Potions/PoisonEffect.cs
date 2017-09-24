using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour {


    public float duration = 5.0f;
    private float poisonRadius;


	private float remainingTime;
	public float fadeOutThreshold = 0.5f;
    private LightFlicker lightFlicker;


    private void Awake()
    {
        lightFlicker = GetComponentInChildren<LightFlicker>();
    }

    private void Start() {
        poisonRadius = 0.3f;
        Destroy(gameObject, duration);
        remainingTime = duration;
	}


    private void FixedUpdate() {
        remainingTime -= Time.fixedDeltaTime;
        if (remainingTime < fadeOutThreshold){
            lightFlicker.SetGeneralStrengh(remainingTime / fadeOutThreshold);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, poisonRadius);
        foreach (Collider2D hit in colliders) {
            if(hit.gameObject.CompareTag("Enemy")){
                hit.gameObject.GetComponent<Enemy>().ApplyDamage(FindObjectOfType<PoisonPotion>().potionDamage, POTION.DOWN);
            }
        }
    }
}
