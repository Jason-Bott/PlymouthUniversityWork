using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public Animation healthBarAnimation;

    //Different sprites for different colour fills
    public Image fill;
    public Sprite fillGreen;
    public Sprite fillYellow;
    public Sprite fillRed;

    private float currentHealth;
    private float maxHealth;

    private bool changingHealth;
    private float healthAim;
    public float healthSpeed;

    void Start()
    {
        //Set the max health and set current health to be the max
        maxHealth = 50;
        currentHealth = maxHealth;
        healthAim = currentHealth;

        changingHealth = false;
    }

    void Update()
    {
        //Check if health needs changing
        if (changingHealth)
        {
            ChangeHealth();
            CheckColour();
        }
    }

    public void TakeDamage(float amount)
    {
        //Lower health aim by the damage taken
        healthAim -= amount;

        //If this kills the player, kill them
        if (healthAim <= 0f)
        {
            Die();
        }

        //Play health bar animation and start changing health
        healthBarAnimation.Play("HealthBarShake");
        changingHealth = true;
    }

    //This method changes the health bar colour if needed
    private void CheckColour()
    {
        //Checks in which range the health bar is in and changed fill colour accordingly
        //If health is more than half set to green
        if ((currentHealth / maxHealth) > 0.5)
        {
            fill.sprite = fillGreen;
        }
        //If health is more than a quarter but less than half set to yellow
        else if ((currentHealth / maxHealth) > 0.25)
        {
            fill.sprite = fillYellow;
        }
        //If health is less than a quarter set to red
        else
        {
            fill.sprite = fillRed;
        }
    }

    //Method to change value of slider over time
    private void ChangeHealth()
    {
        //Checks if health needs to be added
        if (healthAim > currentHealth)
        {
            //Add health based on time between frames
            currentHealth += Time.deltaTime * healthSpeed;

            //Set slider value, current health divided by max health will always give a value for the slider between 0 and 1
            healthBar.value = (currentHealth / maxHealth);

            //Checks if slider value is now greater than the target
            if (currentHealth > healthAim)
            {
                //Sets the slider value to be the target health exactly
                currentHealth = healthAim;
                //Set slider value, current health divided by max health will always give a value for the slider between 0 and 1
                healthBar.value = (currentHealth / maxHealth);
                //Set boolean to false to stop health change
                changingHealth = false;
            }
        }

        //Checks if health needs to be removed
        else if (healthAim < currentHealth)
        {
            //Remove health based on time between frames
            currentHealth -= Time.deltaTime * healthSpeed;

            //Set slider value, current health divided by max health will always give a value for the slider between 0 and 1
            healthBar.value = (currentHealth / maxHealth);

            //Checks if slider value is now lower than the target
            if (currentHealth < healthAim)
            {
                //Sets the slider value to be the target health exactly
                currentHealth = healthAim;
                //Set slider value, current health divided by max health will always give a value for the slider between 0 and 1
                healthBar.value = (currentHealth / maxHealth);
                //Set boolean to false to stop health change
                changingHealth = false;
            }
        }
    }

    void Die()
    {
        //Start player death sequence
        gameObject.GetComponent<ObjectiveManager>().Death();
    }

}
