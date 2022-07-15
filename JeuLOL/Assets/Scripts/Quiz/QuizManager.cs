using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsEtReponses> QeR;
    public GameObject[] options;
    public int QuestionActuelle;

    public GameObject QuizPanel;
    public GameObject GoPanel;

    public Text Questiontext;
    public Text ScoreTxt;
    int TotalQuestions = 0;
    public int score;
    private void Start()
    {
        TotalQuestions = QeR.Count;
        GoPanel.SetActive(false);
        generateQuestions();
    }
    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        QuizPanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxt.text = score + " / " + TotalQuestions;
    }
    public void BonneReponse()
    {
        score += 1;
        QeR.RemoveAt(QuestionActuelle);
        generateQuestions();
    }

    public void Faux()
    {
        QeR.RemoveAt(QuestionActuelle);
        generateQuestions();
    }

    void SetAnswers()
    {
        for(int i = 0; i< options.Length; i++)
        {
            options[i].GetComponent<ScriptReponse>().Correcte = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QeR[QuestionActuelle].Reponses[i];

            if(QeR[QuestionActuelle].RepCorrecte == i+1)
            {
                options[i].GetComponent<ScriptReponse>().Correcte = true;
            }
        }
    }
    void generateQuestions()
    {
        if(QeR.Count > 0)
        {
        QuestionActuelle = Random.Range(0, QeR.Count);

        Questiontext.text = QeR[QuestionActuelle].Question;
        SetAnswers();

        }
        else
        {
            Debug.Log("Out of questions");
            GameOver();
        }
    }
}
