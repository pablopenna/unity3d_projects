using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//para hacerlo Singleton
	public static GameManager instance = null;

	public BoardManager boardScript;

	private int level = 3;

	private bool amItheChosenOne = false;

	//Variables for Player
	//puntuación del jugador?
	public int playerFoodPoints = 100;
	//Aunque sea público, no se mostrara en Unity
	[HideInInspector] public bool playersTurn = true;

	//---

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
			//No he comprobado si funciona el 'this'
			//DontDestroyOnLoad (this);
			DontDestroyOnLoad (gameObject);
			boardScript = GetComponent<BoardManager> ();
			InitGame ();
		}
	}

	void InitGame()
	{
		boardScript.setupScene (level);
	}

	public void GameOver()
	{
		//Here we disable the GameManager (this intance).
		//Disabled instances does not update
		enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}
}