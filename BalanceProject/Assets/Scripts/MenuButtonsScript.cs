using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsScript : MonoBehaviour
{
    public TMP_InputField inputField;
    public void ChangeScene(string sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

    public void createMap()
    {
        Global.name = inputField.text.ToString();
        System.Random rnd = new System.Random();
        MapGenerator.GenerateMap(200, 200, 50, 2, 0.8f, 1, rnd.Next(20, 200), new Vector2(2, 5));
        SceneManager.LoadScene("SampleScene");
    }

    public void continueGame()
    {
        string path = Application.persistentDataPath + "/save.fmm";
        if (File.Exists(path))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void GameQuit()
    {
        Application.Quit();
    }
}
