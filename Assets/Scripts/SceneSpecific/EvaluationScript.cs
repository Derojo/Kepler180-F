using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EvaluationScript : MonoBehaviour
{
    public Text levelStatus;
    public Text endingAurapower;
    public Text endingTurns;
    public Text endingFunds;
    public Text endingEnergy;

    public GameObject levelWonButton;
    public GameObject levelLostButton;

    public Sprite completedLevelIMG;
    public Sprite failedLevelIMG;

    public Image ConditionIMG;

    // Use this for initialization
    void Start ()
    {

        levelWonButton.SetActive(false);
        levelLostButton.SetActive(false);

        if(AuraManager.I.auraLevelPercentage >= 55)
        {
            if(ResourceManager.I.powerLevel == 0)
            {
                ConditionIMG.sprite = failedLevelIMG;
                levelStatus.text = "Level verloren!";
                levelLostButton.SetActive(true);
            }
            else
            {
                ConditionIMG.sprite = completedLevelIMG;
                levelStatus.text = "Level gehaald!";
                levelWonButton.SetActive(true);
            }
          
        }
        if(AuraManager.I.auraLevelPercentage <= 55)
        {
            ConditionIMG.sprite = failedLevelIMG;
            levelStatus.text = "Level verloren!";
            levelLostButton.SetActive(true);
        }
        endingAurapower.text = "Total Aurapower: " + AuraManager.I.currentAuraPower.ToString();
        endingTurns.text = "Total Turns: " + TurnManager.I.turnCount.ToString();
        endingFunds.text = "Total Minerals: " + ResourceManager.I.fundings.ToString();
        endingEnergy.text ="Total Power: " + ResourceManager.I.powerLevel.ToString();    
    }
	
	// Update is called once per frame
	public void ContinueButton ()
    {
        AudioManager.I.source[6].Play();
        LevelManager.I.ResettingValues();
        SceneManager.LoadSceneAsync("LevelSelectionScene");
    }

    public void BackToMain()
    {
        AudioManager.I.source[6].Play();
        LevelManager.I.ResettingValues();
        SceneManager.LoadSceneAsync("StartMenu");
      
  
    }

    public void TryAgain()
    {
        AudioManager.I.source[6].Play();
        Debug.Log("reset values");
        LevelManager.I.ResettingValues();
        SceneManager.LoadSceneAsync("Start");
       
    }
}
