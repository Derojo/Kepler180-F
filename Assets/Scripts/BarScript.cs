using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BarScript : MonoBehaviour {

	[SerializeField]
	private Image auraBar;
    [SerializeField]
    private Image energyBar;
    private bool inBar = false;

	// Update is called once per frame
	void Update () 
	{
		HandleBar ();
	}

	private void HandleBar()
	{
        
        if (auraBar.fillAmount != AuraManager.I.auraLevelPercentage / 100) {
            auraBar.DOFillAmount(AuraManager.I.auraLevelPercentage / 100, 1f).SetEase(Ease.OutSine);
        }
        if (energyBar.fillAmount != ResourceManager.I.powerPercentage / 100)
        {
            if ((ResourceManager.I.powerPercentage / 100) > energyBar.fillAmount)
            {
                energyBar.DOFillAmount(ResourceManager.I.powerPercentage / 100, 1f).SetEase(Ease.OutSine);
                energyBar.DOColor(Color.green, .5f);
            }
            else {
                energyBar.DOColor(Color.white, 1f);
                energyBar.DOFillAmount(ResourceManager.I.powerPercentage / 100, 1f).SetEase(Ease.OutSine);
            }


        }

    }


}
