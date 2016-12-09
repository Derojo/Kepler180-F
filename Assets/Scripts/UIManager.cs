using UnityEngine;
using System.Collections;


public class UIManager : MonoBehaviour {

    public int test = 0;
    [System.Serializable]
    public class ToggleSlider {
        public bool toggleOn = false;

        public void ToggleBoolAnimator(Animator anim) {
            if (!toggleOn)
            {
                anim.SetBool("IsDisplayed", true);
                toggleOn = true;
            }
            else {
                anim.SetBool("IsDisplayed", false);
                toggleOn = false;
            }
        }
    }
    public ToggleSlider toggleSlider;
    public void ToggleBool(Animator anim) {
        toggleSlider.ToggleBoolAnimator(anim);
    }
    public void DisableBoolAnimator(Animator anim)
    {
        anim.SetBool( "IsDisplayed", false );
    }

    public void EnableBoolAnimator(Animator anim)
    {
        anim.SetBool("IsDisplayed", true);
    }

}
