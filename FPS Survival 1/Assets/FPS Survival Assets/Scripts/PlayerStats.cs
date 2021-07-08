using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	public Image health_stats, stamina_stats;


	public void Health_Stats(float health_value)
    {
        health_value /= 100;
        health_stats.fillAmount = health_value;

    }


    public void Stamina_Stats(float stamina_value)
    {
        stamina_value /= 100;
        stamina_stats.fillAmount = stamina_value;
    }


}
