using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [System.Serializable]
    public class Message
    {
        public Types.messages type;
        public GameObject UIObject;
        public bool pulsate;
        public Color pulsateColor;
    }

    private bool inTween = false;
    public GameObject escPopUp;
    public bool menuOpen = false;
    public Message[] Messages = new Message[0];


    public Message getMessageByType(Types.messages type) {
        foreach (Message m in Messages) {
            if (type == m.type) {
                return m;
            }
        }
        return null;
    }

    public void ShowMessage(Types.messages type) {
        Debug.Log(inTween);
        if (!inTween) {
            StartCoroutine(toggleMessage(type));
        }

    }
    public IEnumerator toggleMessage(Types.messages type)
    {
        Message m = getMessageByType(type);
        GameObject UIObject = m.UIObject;
     
        inTween = true;
        if (!m.pulsate)
        {
            UIObject.SetActive(true);
            UIObject.GetComponent<Image>().DOFade(1, 1f);

            foreach (Text text in UIObject.GetComponentsInChildren<Text>())
            {
                text.DOFade(1, 1f);
            }

            foreach (Image img in UIObject.GetComponentsInChildren<Image>())
            {
                img.DOFade(1, 1f);
            }

            yield return new WaitForSeconds(2);

            UIObject.GetComponent<Image>().DOFade(0, 1f);

            foreach (Text text in UIObject.GetComponentsInChildren<Text>())
            {
                text.DOFade(0, 1f);
            }

            foreach (Image img in UIObject.GetComponentsInChildren<Image>())
            {
                img.DOFade(0, 1f);
            }
            yield return new WaitForSeconds(1);


            UIObject.SetActive(false);
        }
        else {
            Color normalColor = UIObject.GetComponent<Image>().color;
            UIObject.GetComponent<Image>().DOColor(m.pulsateColor, .5f);
            UIObject.transform.DOShakeScale(1f,.5f,10,10);
            UIObject.GetComponent<Image>().DOColor(normalColor, .5f).SetDelay(.5f);
            AudioManager.I.source[2].Play();
            yield return new WaitForSeconds(1);
        }

        inTween = false;


    }

    public void ToggleBoolAnimator(Animator anim) {
        bool isDisplayed = anim.GetBool("isDisplayed");
        if (isDisplayed)
        {
            anim.SetBool("isDisplayed", false);
        }
        else {
            anim.SetBool("isDisplayed", true);
        }
    }

    public void EnableBoolAnimator(Animator anim)
    {
        anim.SetBool("isDisplayed", true);
    }

    public void DisableBoolAnimator(Animator anim) {
        anim.SetBool("isDisplayed", false);
    }
}
