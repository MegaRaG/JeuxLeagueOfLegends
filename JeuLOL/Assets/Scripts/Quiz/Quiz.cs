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
	public int nbPoints = 0, numChamp = 0, nbEssai = 0;

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
	private string input;
	
	public void Start()
	{
		var type = Type.GetType("Champ");
		gameObject.AddComponent(type);

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
	}
	public void EnterOnClick()
	{
		nbEssai = 3;
		textFinal.text = "";
		textReponses.text = "";
		Debug.Log("You have clicked the button Start!");
		lesChampions = ChargeChampions();
		Display();
		textSlider.text = $"{numChamp} / 160";

		BaseUrl = championRandom.ImagePath;
		StartCoroutine(LoadImage(BaseUrl));
		
		inputName.text = $"{lesChampions[Generateur.Next(lesChampions.Count)].Nom.ToLower()} ?";
	}
	public void VerifOnClick()
	{
		if (nbEssai < 0)
        {
			textFinal.text = "Vous ne pouvez plus Verif";
		}
		else if (championRandom.Nom.ToUpper() == inputName.text.ToUpper())
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
}
