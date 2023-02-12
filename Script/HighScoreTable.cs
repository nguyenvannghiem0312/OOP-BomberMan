using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    private List<HighScore> highScores = new List<HighScore>();

    public Transform entryContainer;
    public Transform entryTemplate;
    public Text posText, scoreText, nameText;

    private void Awake()
    {
        ReadScore();

        entryTemplate.gameObject.SetActive(false);
        float templateHeight = 30f;
        for(int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            posText.text = (i + 1).ToString();
            scoreText.text = highScores[i].score.ToString();
            nameText.text = highScores[i].name;
        }
    }

    public void ReadScore()
    {
        string path = Application.dataPath + "/Resources/Data.txt";
        foreach (string line in File.ReadLines(path))
        {
            if(line != "")
            {
                highScores.Add(new HighScore { score = int.Parse(line.Split("\t\t")[1]), name = line.Split("\t\t")[0] });
            }       
        }
        highScores.Sort((x, y) => y.score.CompareTo(x.score));
        for (int i = 10; i < highScores.Count; i++)
        {
            highScores.RemoveAt(i);
        }
    }

}
