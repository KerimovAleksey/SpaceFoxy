using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicLevels
{
    public class FoxHealth : HealthComponent
    {
		public override void GetDamage()
		{
			Time.timeScale = 0;
			// Открытие окна конца игры 
		}
	}
}
