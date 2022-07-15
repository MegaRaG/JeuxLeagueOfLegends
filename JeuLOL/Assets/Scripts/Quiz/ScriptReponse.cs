using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptReponse : MonoBehaviour
{
    public bool Correcte = false;
    public QuizManager quizManager;
    public void Answer()
    {
        if(Correcte)
        {
            Debug.Log("Bonne Reponse !");
            quizManager.BonneReponse();
        }
        else
        {
            Debug.Log("Mauvaise Reponse !");
            quizManager.Faux();
        }
    }
}
