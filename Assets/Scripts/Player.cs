using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum POTION {
    UP = 0,
    DOWN = 1,
    LEFT = 2,
    RIGHT = 3
}

public class Player : MonoBehaviour {

    public Potion[] potions;
    public float health;
    public int maxAmmo;
    public float hitDelay = 0.2f;
    public float DamageForce = 1000.0f;
    public float pickUpValue = 0.25f;
    public Transform potionOrigin;
    public GameObject heartPrefab;
    public GameObject healthBar;

    private readonly Vector2[] directions = {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    private Rigidbody2D playerRigidbody;
    private PlayerController playerController;
    private AmmoButtonsAnimator ammoButtonsAnimator;
    private List<GameObject> hearts = new List<GameObject>();

    private float[] ammo;

    private void Awake() {
        potions = new Potion[4];
        potions[(int) POTION.UP] = FindObjectOfType<BoltPotion>();
        potions[(int) POTION.DOWN] = FindObjectOfType<PoisonPotion>();
        potions[(int) POTION.LEFT] = FindObjectOfType<IcePotion>();
        potions[(int) POTION.RIGHT] = FindObjectOfType<FirePotion>();

        ammo = new float[4] { maxAmmo, maxAmmo, maxAmmo, maxAmmo };


        playerRigidbody = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        ammoButtonsAnimator = FindObjectOfType<AmmoButtonsAnimator>();

        for (short i = 0; i < 4; i++) {
            ammoButtonsAnimator.SetValue((POTION) i, Mathf.Floor(ammo[i]) / maxAmmo);
        }

    }

    private void Start() {
        GameObject heartInstance;
        for (int i = 0; i < health; i++) {
            heartInstance = Instantiate(heartPrefab);
            heartInstance.transform.position = Vector2.zero;
            heartInstance.transform.parent = healthBar.transform;
            heartInstance.transform.localPosition = new Vector2(i * 0.75f, 0);
        }
    }

    public void ThrowPotion(POTION type) {
        if (ammo[(int) type] >= 1f) {
            playerController.PlaySound(playerController.throwClip);
            potions[(int) type].InstantiatePotion(potionOrigin.position, directions[(int) type]);
            ammo[(int) type]--;
            ammoButtonsAnimator.SetValue(type, Mathf.Floor(ammo[(int) type]) / maxAmmo);
        }
    }

    public void ApplyDamage(float damageReceived, Vector2 enemyPosition) {
        health -= damageReceived;
        playerRigidbody.AddForce(((Vector2) transform.position - enemyPosition).normalized * DamageForce, ForceMode2D.Force);
        playerController.RestrictMovement(hitDelay);
        playerController.AnimateDamage();
        UpdateHealth();
        playerController.PlaySound(playerController.takeDamageClip);
        if (health <= 0) {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private bool addAmmo(POTION type, float value) {
        if (ammo[(int) type] < maxAmmo) {
            playerController.PlaySound(playerController.pickupClip);
            ammo[(int) type] += value;
            ammoButtonsAnimator.SetValue(type, Mathf.Floor(ammo[(int) type]) / maxAmmo);
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "CollectableBoltPotion":
                if (addAmmo(POTION.UP, pickUpValue)) {
                    Destroy(other.gameObject);
                }
                break;
            case "CollectablePoisonPotion":
                if (addAmmo(POTION.DOWN, pickUpValue)) {
                    Destroy(other.gameObject);
                }
                break;
            case "CollectableIcePotion":
                if (addAmmo(POTION.LEFT, pickUpValue)) {
                    Destroy(other.gameObject);
                }
                break;
            case "CollectableFirePotion":
                if (addAmmo(POTION.RIGHT, pickUpValue)) {
                    Destroy(other.gameObject);
                }
                break;
        }
    }

    private void UpdateHealth() {
        GameObject heartInstance;
        foreach (Transform child in healthBar.GetComponentsInChildren<Transform>()) {
            if (child != healthBar.transform)
                Destroy(child.gameObject);
        }

        for (int i = 0; i < health; i++) {
            heartInstance = Instantiate(heartPrefab);
            heartInstance.transform.position = Vector2.zero;
            heartInstance.transform.parent = healthBar.transform;
            heartInstance.transform.localPosition = new Vector2(i * 0.75f, 0);
        }
    }

}
