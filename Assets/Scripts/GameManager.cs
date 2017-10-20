using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//para hacerlo Singleton
	public static GameManager instance = null;

	public BoardManager boardScript;

	private int level = 3;

	private bool amItheChosenOne = false;

	// Use this for initialization
	void Awake () {
		Debug.Log(instance == null);
		//singleton
		if (instance == null) {
			instance = this;
			amItheChosenOne = true;
		}
		else if (instance != this) {
			Destroy (gameObject);
			//Destroy (this); No funciona con 'this'
		}

		//Solo prosigo si esta es la unica intancia
		if (amItheChosenOne) {

			//para que no se destruya cuando se cargue una scene nueva.
			//que permanezca vivo mientras se ejecute el juego
			//DontDestroyOnLoad (gameObject);
			DontDestroyOnLoad (this);
			boardScript = GetComponent<BoardManager> ();
			InitGame ();
		}
	}

	void InitGame()
	{
		boardScript.setupScene (level);
	}

	// Update is called once per frame
	void Update () {

	}
}