using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Script para que el jugador pueda romper paredes
para no quedarse atrapado o abrirse nuevos caminos*/ 
public class Wall : MonoBehaviour {

	//public variables
	//sprite a mostrar cuando el jugador esté
	//atacando a la pared
	public Sprite dmgSprite;
	public int hp = 4;

	//private variables
	private SpriteRenderer spriteRenderer;

	//----------------------------------------
	// Use this for initialization
	/*ABOUT AWAKE AND START:
	Generally the main difference is:

	Awake: Here you setup the component you are on 
	right now (the "this" object)

	Start: Here you setup things that depend on other components.


	if you split it that way you can always be sure that the 
	other object is ready to work with you as game objects in 
	scenes on scene load are loaded in blocks:

	1. All awake are executed
	2. All start are executed
	3. all go into update.


	Awake is used to initialize any variables or game state 
	before the game starts. Awake is called only once during 
	the lifetime of the script instance. Awake is called after 
	all objects are initialized so you can safely speak to other 
	objects or query them using eg. GameObject.FindWithTag.
	Each GameObject's Awake is called in a random order between 
	objects. Because of this, you should use Awake to set up 
	references between scripts, and use Start to pass any information 
	back and forth. Awake is always called before any Start functions.
	This allows you to order initialization of scripts. 
	Awake can not act as a coroutine.*/
	//----------------------------------------
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	public void damageWall(int loss){
		spriteRenderer.sprite = dmgSprite;
		hp -= loss;
		if(hp <= 0){
			/*gameObject es equivalente a 'this' pero del objeto entero.
			 * this solo se refiere a la clase del script, mientras que
			 * gameObject se refiere a la intancia del GameObject en su totalidad.
			 * El script (la clase) es solo una parte del GameObject.
			*/
			gameObject.SetActive (false);
		}

	}
}
