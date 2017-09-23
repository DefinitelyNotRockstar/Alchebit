using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffect : MonoBehaviour {
    
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
