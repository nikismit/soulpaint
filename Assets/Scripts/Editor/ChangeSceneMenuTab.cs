using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditor.SceneManagement;

public class ChangeSceneMenuTab : EditorWindow {

    [MenuItem("Change Scene/Open MainScene")]
    static void OpenLobby()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/MainScene.unity");
        }
    }

    [MenuItem("Change Scene/hatsumi")]
    static void OpenRoom()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/hatsumi.unity");
        }
    }

}
