using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using RTS_Cam;
using UnityEngine.UI;
using System;

namespace Derojo.Tutorial
{
    public class Tutorial : MonoBehaviour
    {
        #region General
        public int setBeginStep;
        public GameObject Window; // Ui panel that serves as a window so the player cant click anywhere
        public GameObject nextButton;
        public GameObject actionButton;
        public GameObject shopMenu;
        public PlacementController placementController;
        public GameObject arrowPointer;
        private bool completedAction = false;
        private int currentActionId;
        
        #endregion
        #region SetFocus
        public Light directionalLight;
        public Light secondaryLight;
        public Light spotLight;
        public GameObject focusObject;
        public RTS_Camera cam;
        public Material overlay;
        public GameObject overlayObject;
        private float dirIntensity;
        private float secIntensity;

        #endregion
        #region Steps
        public StepsDatabase stepsDB;
        private int currentStepId = 0;
        private Step currentStep;
        #endregion

        void OnEnable()
        {
            EventManager.StartListening("actionFinished", checkIfActionDone);

        }
        //unregistering listeners for clean up
        void OnDisable()
        {
            EventManager.StartListening("actionFinished", checkIfActionDone);
        }

        // Use this for initialization
        private void Start()
        {
            dirIntensity = directionalLight.intensity;
            secIntensity = secondaryLight.intensity;
            StartCoroutine(StartStep(stepsDB.Steps[(setBeginStep-1)]));
        }
        
        // Main tutorial functions, each step has his own procedure
        private void checkIfActionDone()
        {
            if (currentActionId == currentStep.id && !completedAction)
            {
                if (currentStep.eventName != "")
                {
                    if (currentStep.eventName.Contains(","))
                    {
                        string[] eventNames = currentStep.eventName.Split(',');
                        for (int i = 0; i < eventNames.Length; i++)
                        {
                            EventManager.StartListening(eventNames[i], checkIfActionDone);
                        }
                    }
                    else {
                        EventManager.StopListening(currentStep.eventName, checkIfActionDone);
                    }
                    
                }
                if (currentStep.highlightTile != "") {
                    foreach (GameObject placingArrow in GameObject.FindGameObjectsWithTag("Arrow"))
                    {
                        Destroy(placingArrow);
                    }
                }
                disableTurnButton();
                Window.SetActive(true);
                FadeObject(currentStep.actionMessage, false, 0);
                setNextStep();
                completedAction = true;
            }
        }

        private IEnumerator StartStep(Step step)
        {
            Window.SetActive((step.hideWindow ? false : true));
            
            currentStep = step;
            yield return new WaitForSeconds(step.delay);
            cam.cameraState(false);
            if (step.enableCam && !step.hasAction)
            {
                cam.cameraState(true);
            }
            
            GameObject button = (step.hasAction ? actionButton : nextButton);
            //if (step.fadeButton) {
            if (step.id != 58)
            {
                FadeObject(button, true, 1.5f);
            } else
            {
                Manager.I.doneTutorial = true;
            }
            // }

            // General UI show hide
            if (step.highlightUI)
            {
                toggleUIObjects(step, true);
            }
            if (step.highlightObject && step.highlightTile == "")
            {
               setFocusObject(step.highLightGameObject);
            }
            if (step.zoomPos != 0)
            {
                StartCoroutine(ZoomTo(step.zoomPos, step.zoomSpeed));
                if (step.moveToTargetObject)
                {
                    goToObject(step.highLightGameObject, step.zoomOffsetZ);
                }
            } else {
                if (step.moveToTargetObject)
                {
                    goToObject(step.highLightGameObject, step.zoomOffsetZ);
                }
            }
 
            if (step.turnOffLights)
            {
                turnOffLights();
            }
            if (step.unlockTile != "" && !step.hasAction) {
                enablePlacement(step.unlockTile);
            }
            if (step.highlightTile != "" && !step.hasAction) {
                highLightTile(step.highlightTile);
            }
            yield break;
        }

        public void setNextStep()
        {
            if (!RenderSettings.fog)
            {
                RenderSettings.fog = true;
            }

            if (currentStep.turnOnLightsAfter)
            {
                turnOnLights();
            }
            if (currentStep.fadeButton)
            {
                FadeObject(nextButton, false);
            }
           
            if (currentStep.highlightUI)
            {
                toggleUIObjects(currentStep, false);
            }

            if (currentStep.afterFunction != "" && !currentStep.hasAction)
            {
                Invoke(currentStep.afterFunction, 0);
            }

            if (currentStep.UImask != null)
            {
                Destroy(currentStep.UImask);
            }
            Step nextStep = getNextStep(currentStep.nextStep);
            if (nextStep != null)
            {
                StartCoroutine(StartStep(nextStep));
            }

        }

        public void startAction()
        {
            if (currentStep.unlockTile != "")
            {
                enablePlacement(currentStep.unlockTile);
            }
            if (currentStep.highlightTile != "")
            {
                highLightTile(currentStep.highlightTile);
            }
            if (currentStep.afterFunction != "")
            {
                Invoke(currentStep.afterFunction, 0);
            }
            Window.SetActive(false);
            if (currentStep.enableCam)
            {
                cam.cameraState(true);
            }
            if (currentStep.eventName != "") {
                if (currentStep.eventName.Contains(",")) {
                    string[] eventNames = currentStep.eventName.Split(',');
                    for (int i = 0; i < eventNames.Length; i++) {
                        EventManager.StartListening(eventNames[i], checkIfActionDone);
                    }
                } else {
                    EventManager.StartListening(currentStep.eventName, checkIfActionDone);
                }
                
            }
            if (currentStep.fadeButton)
            {
                FadeObject(actionButton, false);
            }
            
            completedAction = false;
            currentActionId = currentStep.id;

            toggleUIObjects(currentStep, false);
            FadeObject(currentStep.actionMessage, true, 1);
           
        }

        private void toggleUIObjects(Step step, bool state)
        {

            if (state)
            {
                if (step.inFade) {
                    RenderSettings.fog = false;
                    overlayObject.SetActive(true);
                    StartCoroutine(FadeOverlay(.8f, 1f));
                }

                if (step.UImask != null) {
                    showMask(step.UImask.GetComponent<Image>(), .5f, true);
                }
                step.UIobject.SetActive(true);
                step.UIobject.transform.DOScale(0, .5f).From();
            }
            else
            {
                if (step.UImask != null)
                {
                    showMask(step.UImask.GetComponent<Image>(), .5f, false);
                }
                step.UIobject.transform.DOScale(0, .5f);
                if (step.outFade)
                {
                    StartCoroutine(FadeOverlay(0, 1f));
                    overlayObject.SetActive(false);
                }
            }

        }

        private Step getNextStep(int stepId)
        {
            foreach (Step step in stepsDB.Steps)
            {
                if (step.id == stepId)
                {
                    return step;
                }
            }
            return null;
        }



        private void hideGoals()
        {
            GetComponent<ExecutionTutorial>().hideGoals();
        }

        private void FadeObject(GameObject ob, bool state, float delay = 0)
        {
            if (state)
            {
                ob.SetActive(true);
                ob.GetComponent<Image>().DOFade(1, 1).SetDelay(delay);
                ob.transform.GetChild(0).GetComponent<Text>().DOFade(1, 1).SetDelay(delay);
            }
            else
            {
                ob.GetComponent<Image>().DOFade(0, 1).SetDelay(delay);
                ob.transform.GetChild(0).GetComponent<Text>().DOFade(0, 1).SetDelay(delay);

                ob.SetActive(false);
            }

        }

        // Focusing functions, lights - go to object etc
        private void turnOffLights()
        {
            directionalLight.DOIntensity(0, 2).SetEase(Ease.InSine);
            secondaryLight.DOIntensity(0, 2).SetEase(Ease.InSine);
            RenderSettings.ambientIntensity = 0;
            RenderSettings.ambientLight = Color.black;
        }

        private void turnOnLights()
        {
            directionalLight.DOIntensity(dirIntensity, 1).SetEase(Ease.OutBack);
            secondaryLight.DOIntensity(secIntensity, 1).SetEase(Ease.OutBack);
            RenderSettings.ambientIntensity = 0.2f;
        }


        private void setFocusObject(GameObject focusObject)
        {
            Vector3 spotLightPos = new Vector3(focusObject.transform.position.x, spotLight.transform.position.y, focusObject.transform.position.z);
            spotLight.transform.position = spotLightPos;
            spotLight.gameObject.SetActive(true);
            spotLight.DOIntensity(5, 1).SetEase(Ease.InSine);
        }

        private void goToObject(GameObject focus, float offset = 0)
        {
            cam.targetOffset = new Vector3(cam.targetOffset.x, cam.targetOffset.y, (offset == 0 ? cam.targetOffset.z : offset));
            cam.SetTarget(focus.transform);
            // use function below to pause the camera
            //cam.cameraState(true); 
        }

        private IEnumerator ZoomTo(float zoomPos, float duration) {


            for (float t = 0.0f; t < 1.0f; t+= Time.deltaTime/duration)
            {
                if (Mathf.Round(cam.zoomPos * 1000.0f) / 1000.0f == Mathf.Round(zoomPos * 1000.0f) / 1000.0f) {
                    yield break;
                }
                cam.zoomPos = Mathf.Lerp(cam.zoomPos, zoomPos, t * cam.heightDampening);
                yield return null;
            }
            yield break;

        }

        IEnumerator FadeOverlay(float aValue, float aTime)
        {
            float alpha = overlay.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
                overlay.color = newColor;
                yield return null;
            }
            yield break;
        }

        private void showMask(Image mask, float duration, bool show = true)
        {
            if (show)
            {
                mask.gameObject.SetActive(true);
                mask.transform.DOScale(0f, duration).From().SetEase(Ease.OutCirc);
            }
            else
            {
                mask.transform.DOScale(0f, duration).SetEase(Ease.InCirc);
                mask.gameObject.SetActive(false);
            }

        }



        //**** OVERRIDE FUNCTIONS ***********/

        public void StartPlacement(GameObject building) {
            if (building.name == "Redgenerator") {
                EventManager.TriggerEvent("placeRed");
            }   
            placementController.StartPlacement(building);
        }

        /******** AFTER FUNCTIONS ***********/
        public void enableRed() {
            shopMenu.transform.GetChild(0).GetComponent<Button>().interactable = true;
            shopMenu.transform.GetChild(1).GetComponent<Button>().interactable = false;
            shopMenu.transform.GetChild(2).GetComponent<Button>().interactable = false;
            shopMenu.transform.GetChild(3).GetComponent<Button>().interactable = false;
        }

        public void enableBlue()
        {
            shopMenu.transform.GetChild(1).GetComponent<Button>().interactable = true;
            shopMenu.transform.GetChild(0).GetComponent<Button>().interactable = false;
            shopMenu.transform.GetChild(2).GetComponent<Button>().interactable = false;
            shopMenu.transform.GetChild(3).GetComponent<Button>().interactable = false;
        }

        public void enableYellow()
        {
            shopMenu.transform.GetChild(0).GetComponent<Button>().interactable = false;
            shopMenu.transform.GetChild(1).GetComponent<Button>().interactable = false;
            shopMenu.transform.GetChild(2).GetComponent<Button>().interactable = false;
            shopMenu.transform.GetChild(3).GetComponent<Button>().interactable = true;
        }

        public void disableShop()
        {
            shopMenu.transform.GetChild(0).GetComponent<Button>().interactable = false;
            shopMenu.transform.GetChild(1).GetComponent<Button>().interactable = false;
            shopMenu.transform.GetChild(2).GetComponent<Button>().interactable = false;
            shopMenu.transform.GetChild(3).GetComponent<Button>().interactable = false;
        }

        public void enableBuy() {
            GameObject.Find("InfoPanel").transform.GetChild(0).GetChild(10).GetComponent<Button>().interactable = true;
        }

        public void enableTurnButton()
        {
            GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = true;
        }

        public void disableTurnButton()
        {
            GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = false;
        }

        public void enablePlacement(string tileName) {
            string[] arr = tileName.Split('|');
            Tile tile = Grid.getTileAtPosition(int.Parse(arr[0]), int.Parse(arr[1]));
            tile.locked = false;
        }

        public void highLightTile(string tileName)
        {
            List<string[]> stringTiles = new List<string[]>();
            string[] tilesString;
            if (tileName.Contains(","))
            {
                tilesString = tileName.Split(',');
                for (int i = 0; i < tilesString.Length; i++) {
                    stringTiles.Add(tilesString[i].Split('|'));
                }
            }
            else {
                stringTiles.Add(tileName.Split('|'));
            }

            foreach (string[] tileString in stringTiles) {
                Tile tile = Grid.getTileAtPosition(int.Parse(tileString[0]), int.Parse(tileString[1]));

                if (currentStep.highlightObject)
                {
                    setFocusObject(tile.currentObject);
                }
                else
                {
                    GameObject arrow = Instantiate(arrowPointer);
                    arrow.name = arrowPointer.name;
                    arrow.transform.localScale = arrowPointer.transform.localScale;
                    arrow.transform.position = new Vector3(tile.transform.position.x, arrowPointer.transform.position.y, tile.transform.position.z);
                    arrow.transform.DOMoveY(arrowPointer.transform.position.y + 10, 1f).SetLoops(-1, LoopType.Yoyo);
                }
            }
        }

        public void hideBluePrint() {
            GameObject.Find("ResumeGame").GetComponent<Button>().onClick.Invoke();
        }

    }
}