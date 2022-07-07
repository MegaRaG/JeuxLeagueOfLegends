﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
	List<Champ> lesChampions = new List<Champ>();
	Champ championRandom;
    System.Random Generateur = new System.Random();
	List<Champ> passé = new List<Champ>();
	int nbPoints = 0, numChamp = 0, nbEssai = 0;

	public Button buttonStart;
	public Button buttonVerif;
	public InputField inputName;
	public Text textFinal;
	public Text textReponses;
	public Text textPoints;
	public Text textSlider;
	public string BaseUrl;
	public RawImage Image;

	//public GameObject loading;
	private float targetProgress = 0;
	public float FillSpeed = 0.5f;
	private ParticleSystem particuleSys;
	private Slider slider;
	private string input;
	private float valBail;
	public void Start()
	{
		Button btn = buttonStart.GetComponent<Button>();
		btn.onClick.AddListener(EnterOnClick);
		Button btnn = buttonVerif.GetComponent<Button>();
		btnn.onClick.AddListener(VerifOnClick);
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			textFinal.text = championRandom.Nom;
		}
		if(slider.value < targetProgress)
        {
			slider.value += FillSpeed*Time.deltaTime;
			if (!particuleSys.isPlaying)
				particuleSys.Play();
        }
        else {
			particuleSys.Stop(); 
		}

	}
	public void EnterOnClick()
	{
		textFinal.text = "";
		textReponses.text = "";
		Debug.Log("You have clicked the button Start!");
		lesChampions = ChargeChampions();
		Display();
		textSlider.text = $"{numChamp} / 160";

		BaseUrl = championRandom.ImagePath;
		StartCoroutine(LoadImage(BaseUrl));

		nbEssai = 3;

		valBail = numChamp / 160;
		valBail = convert(valBail);
		
		IncrementProgress(valBail);
		inputName.text = $"{lesChampions[Generateur.Next(lesChampions.Count)].Nom.ToLower()} ?";
	}
	public float convert(float f)
    {
		string bai = f.ToString();
		bai = bai + "f";
		f = float.Parse(bai);
		return f;
    }
	public void VerifOnClick()
	{
		if (nbEssai < 0)
        {
			textFinal.text = "Vous ne pouvez plus Verif";
		}
		if (championRandom.Nom.ToUpper() == inputName.text.ToUpper())
		{
			textReponses.text = $"Bien joué, c'est bien {championRandom.Nom} !";
			nbPoints = nbPoints + 1;
			passé.Add(championRandom);
		}
		else if (nbEssai == 2 || nbEssai == 1)
		{
			nbEssai = nbEssai-1;
			textReponses.text  = $"Aïe, c'est pas {inputName.text.ToUpper()} \nplus que {nbEssai} essais !";
		}
		else if (nbEssai == 0)
		{
			textReponses.text = $"Nuuuul, c'était {championRandom.Nom}";
			passé.Add(championRandom);
		}
		textPoints.text = $"Points : {nbPoints}";
	}
	public void ReadStringInput(string s)
	{
		input = s.ToUpper();
		Debug.Log("Input = " + input);
	}

	public static List<Champ> ChargeChampions()
	{
		List<Champ> lesStocks = null;
		try
		{
			string contenuFichier = File.ReadAllText("Assets/Scripts/LoLJSON.json");
			lesStocks = JsonConvert.DeserializeObject<List<Champ>>(contenuFichier);
		}
		catch (Exception e) { throw e; }
		return lesStocks;
	}
	private void Display()
	{

		if (passé.Count == lesChampions.Count)
		{
			textFinal.text = "Vous avez fais tous les champions !";
		}
		do
		{
			championRandom = lesChampions[Generateur.Next(lesChampions.Count)];
			numChamp+=1;
		} while (passé.Contains(championRandom) && passé.Count != lesChampions.Count);
	}
	IEnumerator LoadImage(string imageURL)
	{
		WWW www = new WWW(imageURL);
		//loading.SetActive(true);
		yield return www;
		if (www.error == null)
		{
			//loading.SetActive(false);
			Texture2D texture = www.texture;
			Image.texture = texture;
		}
		else
		{
			Debug.Log("Ya une erreur URL");
		}
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
}
