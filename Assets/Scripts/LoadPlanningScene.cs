using System.Collections;
using UnityEngine;

public class LoadPlanningScene : MonoBehaviour {


	public void ChangeToScene (string sceneToChangeTo) {
        Application.LoadLevel(sceneToChangeTo);
	}
}
