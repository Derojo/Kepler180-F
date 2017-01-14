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
    }

    private bool inTween = false;
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
        if (!inTween) {
            StartCoroutine(toggleMessage(type));
        }

    }
    public IEnumerator toggleMessage(Types.messages type)
    {
        Message m = getMessageByType(type);
        Debug.Log(m.UIObject.name);
        GameObject UIObject = m.UIObject;
        UIObject.SetActive(true);
        inTween = true;

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

        inTween = false;
        UIObject.SetActive(false);

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
