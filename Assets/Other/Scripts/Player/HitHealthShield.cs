using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitHealthShield : MonoBehaviour {

    public float regainShield = 8;   // 
    public float regainHealth = 4;   // 

    public Slider shieldSlider;
    public Slider healthSlider;

    public Image hitEffect;
    public float hitEffectDuration = 1.0f;  // in seconds
    float hitEffectTime = 0;
    const float hitAlpha = 0.3f;


	// Use this for initialization
	void Start () {
        if (shieldSlider == null || healthSlider == null)
            Debug.Log("HitHealthShield.cs: sliders are null!");
	    
	}

    // Update is called once per frame
    void Update() {
        float shield = shieldSlider.value;
        float health = healthSlider.value;

        if (shield < 100)
        {
            shield += regainShield * Time.deltaTime;
            if (shield > 100)
                shield = 100;
            shieldSlider.value = shield;
            //Debug.Log("Shield: " + shield);
        }

        if (health < 100)
        {
            health += regainHealth * Time.deltaTime;
            if (health > 100)
                health = 100;
            healthSlider.value = health;
            //Debug.Log("health: " + health);
        }

        if (hitEffect.enabled)
        {
            if (hitEffectTime <= 0)
                hitEffect.enabled = false;
            hitEffectTime -= Time.deltaTime;
            Color c = hitEffect.color;
            c.a -= Time.deltaTime * hitAlpha / hitEffectDuration;
            hitEffect.color = c;
        }

        // TODO: debug only!
        if (Input.GetKeyDown(KeyCode.X))
            Hit(10);
	}

    public void Hit(int strength)   // how many % does it take
    {
        float shield = shieldSlider.value;
        float health = healthSlider.value;


        shield -= strength;
        if (shield < 0)
        {
            health += shield;
            shield = 0;
        }
        if (health <= 0)
        {
            health = 0;
            // TODO: player dies
            Debug.Log("Player dies!");
        }

        if (shield != shieldSlider.value)
        {
            hitEffect.enabled = true;
            Color c = Color.green;
            if (health != healthSlider.value)
                c = Color.red;
            c.a = hitAlpha;
            hitEffect.color = c;
            hitEffectTime = hitEffectDuration;
        }

        /*if (health != healthSlider.value)
        {
            hitEffect.enabled = true;
            Color c = hitEffect.color;
            c.a = hitAlpha;
            hitEffect.color = c;
            hitEffectTime = hitEffectDuration;
        }*/

        shieldSlider.value = shield;
        healthSlider.value = health;
    }
}
