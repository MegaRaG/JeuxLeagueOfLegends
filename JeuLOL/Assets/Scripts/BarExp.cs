using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarExp : MonoBehaviour
{
    Quiz quizz = new Quiz();

    private float targetProgress = 0;
    public float FillSpeed = 0.5f;
    private ParticleSystem particuleSys;
    private Slider slider;
    int valeur = 0;
    float valBail;

    // Update is called once per frame
    void Update()
    {
        if(quizz.numChamp > valeur && quizz.numChamp < 161)
        {
            valBail = quizz.numChamp / 160;
            valBail = convert(valBail);
            IncrementProgress(valBail);
        }
         
        if (slider.value < targetProgress)
        {
            slider.value += FillSpeed * Time.deltaTime;
            if (!particuleSys.isPlaying)
                particuleSys.Play();
        }
        else
        {
            particuleSys.Stop();
        }

     valeur = quizz.numChamp;
    }
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        particuleSys = GameObject.Find("Progress Bar Particules").GetComponent<ParticleSystem>();
    }
    private void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }
    public float convert(float f)
    {
        string bai = f.ToString();
        bai = bai + "f";
        f = float.Parse(bai);
        return f;
    }
}
