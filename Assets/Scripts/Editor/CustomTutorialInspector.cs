using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Derojo.Tutorial.Editors
{
    [CustomEditor(typeof(StepsDatabase))]
    public class CustomTutorialInspector : Editor
    {
        StepsDatabase db;
        int tempShowId = 0;
        void OnEnable() {
            db = (StepsDatabase)target;
        }

        public override void OnInspectorGUI()
        {

         
            GUILayout.Label("Total Tutorial Steps "+db.Steps.Count);
            GUILayout.BeginHorizontal();
            


            if (GUILayout.Button("Fold all in"))
                FoldIn();
            if (GUILayout.Button("Show"))
                Show(tempShowId);
            if (GUILayout.Button("Hide"))
                Hide(tempShowId);
            tempShowId = EditorGUILayout.IntField(tempShowId, GUILayout.Width(100));
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Add Tutorial Step"))
                AddStep();
            GUILayout.Space(20);
            for (int i = 0; i < db.Steps.Count; i++) {
                /***************** HEADER **********************/

                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                    EditorGUILayout.Space();
                    db.Steps[i].showSettings = EditorGUILayout.Foldout(db.Steps[i].showSettings, "Step "+ (i + 1) + ". " + db.Steps[i].title +" (id:"+ db.Steps[i].id+")", EditorStyles.foldout);

                    if (GUILayout.Button("X", GUILayout.Width(20)))
                    {
                        RemoveStep(i);
                        return;
                    }
                    
                
                GUILayout.EndHorizontal();

                if (db.Steps[i].showSettings)
                {
                    GUILayout.BeginHorizontal();

                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    EditorGUILayout.LabelField("General:", EditorStyles.boldLabel, GUILayout.Width(100));

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Stepname:", GUILayout.Width(100));
                    db.Steps[i].title = GUILayout.TextField(db.Steps[i].title, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("NextStep: ", GUILayout.Width(100));
                    db.Steps[i].nextStep = EditorGUILayout.IntField(db.Steps[i].nextStep, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Delay:", GUILayout.Width(100));
                    db.Steps[i].delay = EditorGUILayout.FloatField(db.Steps[i].delay, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Enable Cam:", GUILayout.Width(100));
                    db.Steps[i].enableCam = EditorGUILayout.Toggle(db.Steps[i].enableCam, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("FadeIn:", GUILayout.Width(100));
                    db.Steps[i].inFade = EditorGUILayout.Toggle(db.Steps[i].inFade, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("FadeOut:", GUILayout.Width(100));
                    db.Steps[i].outFade = EditorGUILayout.Toggle(db.Steps[i].outFade, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Hide Window:", GUILayout.Width(100));
                    db.Steps[i].hideWindow = EditorGUILayout.Toggle(db.Steps[i].hideWindow, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();

                    /***************** UI VERTICAL SETTINGS **********************/
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    EditorGUILayout.LabelField("UI Step:", EditorStyles.boldLabel, GUILayout.Width(100));

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("UseUIHighlight?:", GUILayout.Width(100));
                    db.Steps[i].highlightUI = EditorGUILayout.Toggle(db.Steps[i].highlightUI, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("UI Object:", GUILayout.Width(100));

                    db.Steps[i].UIobject = (GameObject)EditorGUILayout.ObjectField(db.Steps[i].UIobject, typeof(GameObject));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("UI Mask:", GUILayout.Width(100));
                    db.Steps[i].UImask = (GameObject)EditorGUILayout.ObjectField(db.Steps[i].UImask, typeof(GameObject));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("FadeOutButton:", GUILayout.Width(100));
                    db.Steps[i].fadeButton = EditorGUILayout.Toggle(db.Steps[i].fadeButton, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.Space(10);
                    /***************** INGAME OBJECTS VERTICAL SETTINGS **********************/
                    EditorGUILayout.LabelField("InGame Step:", EditorStyles.boldLabel, GUILayout.Width(100));


                    GUILayout.BeginHorizontal();
                    GUILayout.Label("UseObjectHighlight?:", GUILayout.Width(100));
                    db.Steps[i].highlightObject = EditorGUILayout.Toggle(db.Steps[i].highlightObject, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("highLightGameObject:", GUILayout.Width(100));
                    db.Steps[i].highLightGameObject = (GameObject)EditorGUILayout.ObjectField(db.Steps[i].highLightGameObject, typeof(GameObject));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("moveToTargetObject:", GUILayout.Width(100));
                    db.Steps[i].moveToTargetObject = EditorGUILayout.Toggle(db.Steps[i].moveToTargetObject, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("turnOffLights:", GUILayout.Width(100));
                    db.Steps[i].turnOffLights = EditorGUILayout.Toggle(db.Steps[i].turnOffLights, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("turnOnnLightsAfter:", GUILayout.Width(100));
                    db.Steps[i].turnOnLightsAfter = EditorGUILayout.Toggle(db.Steps[i].turnOnLightsAfter, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Zoom position:", GUILayout.Width(100));
                    db.Steps[i].zoomPos = EditorGUILayout.FloatField(db.Steps[i].zoomPos, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Zoom speed:", GUILayout.Width(100));
                    db.Steps[i].zoomSpeed = EditorGUILayout.FloatField(db.Steps[i].zoomSpeed, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Zoom OffsetZ:", GUILayout.Width(100));
                    db.Steps[i].zoomOffsetZ = EditorGUILayout.FloatField(db.Steps[i].zoomOffsetZ, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Highlight Tile:", GUILayout.Width(100));
                    db.Steps[i].highlightTile = GUILayout.TextField(db.Steps[i].highlightTile, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Unlock Tile:", GUILayout.Width(100));
                    db.Steps[i].unlockTile = GUILayout.TextField(db.Steps[i].unlockTile, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    /***************** Actions VERTICAL SETTINGS **********************/
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    EditorGUILayout.LabelField("Actions&Functions:", EditorStyles.boldLabel, GUILayout.Width(150));

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Afterfunction:", GUILayout.Width(100));
                    db.Steps[i].afterFunction = GUILayout.TextField(db.Steps[i].afterFunction, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("HasAction?:", GUILayout.Width(100));
                    db.Steps[i].hasAction = EditorGUILayout.Toggle(db.Steps[i].hasAction, GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("ActionMessage:", GUILayout.Width(100));
                    db.Steps[i].actionMessage = (GameObject)EditorGUILayout.ObjectField(db.Steps[i].actionMessage, typeof(GameObject));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("EventName:", GUILayout.Width(100));
                    db.Steps[i].eventName = GUILayout.TextField(db.Steps[i].eventName);
                    GUILayout.EndHorizontal();

                    GUILayout.EndVertical();

                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.Space(20);
            if (GUILayout.Button("Add Tutorial Step"))
                AddStep();
            if (GUI.changed) {
                EditorUtility.SetDirty(db);
                AssetDatabase.SaveAssets();
            }
        }

        void AddStep() {
            db.Steps.Add(new Step(db.Steps.Count));
        }

        void FoldIn() {
            for (int i = 0; i < db.Steps.Count; i++)
            {
                db.Steps[i].showSettings = false;
            }
        }
        void Show(int id) {
            db.Steps[(id-1)].showSettings = true;
        }

        void Hide(int id)
        {
            db.Steps[(id - 1)].showSettings = false;
        }

        void RemoveStep(int id) {
            db.Steps.RemoveAt(id);
        }
    }
}