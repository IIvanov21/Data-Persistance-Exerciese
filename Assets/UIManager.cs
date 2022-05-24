using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InputField playerInputField;
    public Text warningText;
   

    public void SetInputField()
    {
        playerInputField.text = DataManager.Instance.playerName;

    }
    public void SetPlayerName()
    {
        if (playerInputField.text == "")
        {
            warningText.gameObject.SetActive(true);
            Debug.Log("Please enter player name!");
        }
        else DataManager.Instance.playerName = playerInputField.text;
    }
    public void LoadMainScene()
    {
        if (playerInputField.text != "") SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
