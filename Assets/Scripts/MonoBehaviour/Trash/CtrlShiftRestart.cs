using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CtrlShiftRestart : MonoBehaviour
{
    private static CtrlShiftRestart _instance;

    public static CtrlShiftRestart Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject().AddComponent<CtrlShiftRestart>();
            }
            return _instance;
        }
        private set { if (_instance != null) _instance = value; }
    }

    internal void Start()
    {
#if UNITY_EDITOR
        EditorApplication.update += Update;
#endif
    }

    internal void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            if (Input.GetKey(KeyCode.LeftShift))
                if (Input.GetKeyDown(KeyCode.R))
                    ReloadScene();
    }

    private void ReloadScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name, LoadSceneMode.Single);
    }

    internal void OnGUI()
    {
        float height = 50;
        float width = 100;
        if (GUI.Button(new Rect(Screen.width / 2 - width, 0, width, height), "Restart"))
        {
            ReloadScene();
        }
    }
}