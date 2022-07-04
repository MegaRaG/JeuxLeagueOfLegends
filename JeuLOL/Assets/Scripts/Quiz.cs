using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
	public Button buttonStart;
	public InputField inputName;
	private string input;
	public void Start()
	{
		Button btn = buttonStart.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	public void TaskOnClick()
	{
		Debug.Log("You have clicked the button Start!");
	}

	public void ReadStringInput(string s)
	{
		input = s;
		Debug.Log("Input = " + input);
	}
}
