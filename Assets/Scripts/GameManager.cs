using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//para hacerlo Singleton
	public static GameManager instance = null;

	public BoardManager boardScript;

	private int level = 3;

	// Use this for initialization
	void Awake () {

		//singleton
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		//para que no se destruya cuando se cargue una scene nueva.
		//que permanezca vivo mientras se ejecute el juego
		DontDestroyOnLoad (gameObject);
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame()
	{
		boardScript.setupScene (level);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
