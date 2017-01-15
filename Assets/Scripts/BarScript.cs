using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	[SerializeField]
	private Image auraBar;
    [SerializeField]
    private Image energyBar;

	// Update is called once per frame
	void Update () 
	{
		HandleBar ();
	}

	private void HandleBar()
	{
        if (auraBar.fillAmount != AuraManager.I.auraLevelPercentage / 100) {
            auraBar.fillAmount = AuraManager.I.auraLevelPercentage / 100;
        }
        if (energyBar.fillAmount != ResourceManager.I.powerPercentage / 100)
        {
            energyBar.fillAmount = ResourceManager.I.powerPercentage / 100;
        }

    }


}
