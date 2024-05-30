using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
// keep this as public to be shared by anything you want with an hp bar

public class HealthBar : MonoBehaviour

{
	public Slider slider;
	//public Gradient gradient;
	public Image fill;

	public void SetMaxHealth(float health)
	{
		slider.maxValue = health;
		slider.value = health;

		//fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth(float health)
	{
		slider.value = health;

		//fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
// this is how you would use the Hp bar in code with out refencing it 

/*public HealthBar healthBar;

public int maxHealth = 100;
public int currentHealth;

void Start()
{
currentHealth= maxHealth;
healthBar.SetMaxHealth(maxHealth);
}

void Update ()
{
if (Input GetKeyDown(KeyCode Space))
{
 {
    TakeDamage (20);
 }
}
void TakeDamage(int damage)
{
    currentHealth-= damage；
healthBar. SetHealth(currentHealth);
}
*/

