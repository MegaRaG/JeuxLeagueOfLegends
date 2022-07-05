using Newtonsoft.Json;
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
	int nbPoints = 0, numChamp = 0, nbEssai = 3;

	public Button buttonStart;
	public Button buttonVerif;
	public InputField inputName;
	public Text textFinal;
	public Text textReponses;
	public Text textPoints;
	public string BaseUrl;
	//public GameObject loading;
	public RawImage Image;
	private string input;
	public void Start()
	{
		lesChampions = ChargeChampions();
		Display();

		Button btn = buttonStart.GetComponent<Button>();
		btn.onClick.AddListener(EnterOnClick);
		Button btnn = buttonVerif.GetComponent<Button>();
		btnn.onClick.AddListener(VerifOnClick);
	}

	public void EnterOnClick()
	{
		textFinal.text = championRandom.Nom;
		BaseUrl = championRandom.ImagePath;
		StartCoroutine(LoadImage(BaseUrl));
		nbEssai = 3;
		Debug.Log("You have clicked the button Start!");

		Display();
		inputName.text = $"{lesChampions[Generateur.Next(lesChampions.Count)].Nom.ToLower()} ?";

	}

	public void VerifOnClick()
	{
		if(nbEssai == 0)
        {
			textReponses.text = $"Nuuuul, c'était {championRandom.Nom}";
		}

		if (championRandom.Nom.ToUpper() == inputName.text.ToUpper())
		{
			textReponses.text = $"Bien joué, c'est bien {championRandom.Nom} !";
			nbPoints += 2;
		}
		else
        {
			nbEssai--;
			textReponses.text  = $"Aïe, c'est pas {inputName.text.ToUpper()} \nplus que {nbEssai} essais !";
		}

		passé.Add(championRandom);
	}
	public void ReadStringInput(string s)
	{
		input = s;
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
			numChamp++;
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
}
