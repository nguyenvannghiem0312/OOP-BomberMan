using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject PanelWin;  // Maybe not need
    public GameObject PanelLose; // Maybe not need
    public GameObject PanelSetting;
    public GameObject PanelEndGame;

    [Header("Number HP and Bomb")]
    public Text numBomb;
    public Text numHP;

    [Header("Score")]
    public Text score;

    [Header("Input Name")]
    public InputField saveName;

    public void ChangeScene(int SceneID)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneID);
    }
    public void ShowPanel(GameObject Panel, bool panel)
    {
        if (Panel)
        {
            Panel.SetActive(panel);
        }
    }
    public void CloseApp()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }
    public void SetScore(int point)
    {
        score.text = "Score: " + (int.Parse(score.text.Remove(0, 7)) + point).ToString();
    }
    public int GetScore()
    {
        return int.Parse(score.text.Remove(0, 7));
    }
    public void SetBomb(int numbomb)
    {
        numBomb.text = (numbomb).ToString();
    }
    public void SetHP(int numhp)
    {
        numHP.text = (numhp).ToString();
    }
    public void ShowSetting(bool state)
    {
        if(state == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        ShowPanel(PanelSetting, state);
    }

    public string GetName()
    {
        return saveName.text + "\t\t" + score.text.Remove(0, 7);
    }
}
