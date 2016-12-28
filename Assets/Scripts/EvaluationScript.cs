using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EvaluationScript : MonoBehaviour
{
    public Text levelStatus;
    public Text endingAurapower;
    public Text endingTurns;
    public Text endingFunds;
    public Text endingEnergy;

	// Use this for initialization
	void Start ()
    {
        if(AuraManager.I.auraLevelPercentage >= 55)
        {
            levelStatus.text = "Level gehaald!";
        }
        if(AuraManager.I.auraLevelPercentage <= 55)
        {
            levelStatus.text = "Level verloren!";
        }
        endingAurapower.text = "Total Aurapower: " + AuraManager.I.currentAuraPower.ToString();
        endingTurns.text = "Total Turns: " + TurnManager.I.turnCount.ToString();
        endingFunds.text = "Total Minerals: " + ResourceManager.I.fundings.ToString();
        endingEnergy.text ="Total Power: " + ResourceManager.I.powerLevel.ToString();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
