using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	public GameObject gameManager;

	void Awake () {
		/*
		if (GameManager.instance == null) {
			Instantiate (gameManager);
		}
		*/

		for (int i = 0; i < 3; i++) {
			Instantiate (gameManager);
		}
	}

}
