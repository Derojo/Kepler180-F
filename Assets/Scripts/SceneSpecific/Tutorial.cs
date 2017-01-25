using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using RTS_Cam;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour {

    
    public Light directionalLight;
    public Light secondaryLight;
    public Light spotLight;
    public GameObject focusObject;
    public RTS_Camera cam;
    public Material overlay;

    [System.Serializable]
    public class Step {
        public int id = 0;
        public bool highlightUI;
        public GameObject UIobject;
        public bool highlightObject;
        public GameObject inGameObject;
    }
    public List<Step> Steps = new List<Step>();

    private float dirIntensity;
    private float secIntensity;
    private int currentStep = 0;



    // Use this for initialization
    private void Start () {

        dirIntensity = directionalLight.intensity;
        secIntensity = secondaryLight.intensity;
        StartStep(getCurrentStep());
        //StartCoroutine(FadeOverlay(.8f, 1f));
        //showMask(GameObject.Find("Mask").GetComponent<Image>(), 1, false);
    }

    // Main tutorial functions, each step has his own procedure

    private void StartStep(Step step)
    {
        if (step.id == 0)
        {
            Debug.Log("start goal step");
        }
    }

    private Step getCurrentStep()
    {
        foreach (Step step in Steps)
        {
            if (step.id == currentStep)
            {
                return step;
            }
        }
        return null;
    }

    private void setNextStep()
    {
        currentStep++;

        StartStep(getCurrentStep());
    }

    // Focusing functions, lights - go to object etc
    private void turnOffLights() {
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

    private IEnumerator ieSetFocus (float time) {
        yield return new WaitForSeconds(time);
        setFocusObject();
        goToObject(focusObject);
    }

    private void setFocusObject() {
        Vector3 spotLightPos = new Vector3(focusObject.transform.position.x, spotLight.transform.position.y, focusObject.transform.position.z);
        spotLight.transform.position = spotLightPos;
        spotLight.gameObject.SetActive(true);
        spotLight.DOIntensity(5, 1).SetEase(Ease.InSine);
    }

    private void goToObject(GameObject focus)
    {
        cam.SetTarget(focus.transform);
        // use function below to pause the camera
        //cam.camereState(true); 
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
    }

    private void showMask(Image mask, float duration, bool show = true) {
        if (show)
        {
            mask.transform.DOScale(0f, duration).From().SetEase(Ease.OutCirc);
        }
        else {
            mask.transform.DOScale(0f, duration).SetEase(Ease.InCirc);
        }

    }

}
