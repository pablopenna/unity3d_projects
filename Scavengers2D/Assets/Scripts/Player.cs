using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Este script es un componente añadido a un GameObject.
-> Mediante 'this' accedemos a este componente (script Player.cs)
-> Mediante 'gameObject' accedemos la instancia de GameObjcet de la que cuelga
este componente. Así podemos obetener otros componentes como el animador, el
rigidbody, etc.
*/



//Player inherits from MovingObject
public class Player : MovingObject {

	//Damage done to walls when the Player chops them
	public int wallDamage = 1;
	//Puntos obtenidos al recoger la comida
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;
	//?
	public float restarLevelDelay = 1f;

	//Aqui alamcenaremos una referencia a nuestro componente Animator
	private Animator animator;

	//almacenar nuestra puntuacion durante la partida
	//HP
	private int food;

	// Use this for initialization
	//protected override -> ya que vamos a emplear una implementción
	//diferente de la que tiene MovingObject de Start()
	protected override void Start () {

		animator = GetComponent<Animator> ();
		//Obtenemos la puntuación del GameManager
		food = GameManager.instance.playerFoodPoints;

		//LLAMAMOS A LA FUNCION Start() de MovingObject, la clase de la que heredamos
		base.Start();
	}

	//Parete de la API de Unity.
	//Se llama cuando el GameObject Player sea desactivado.
	private void OnDisable()
	{
		//Guardamos la puntucaión de forma que se mantenga al cambiar de niveles
		GameManager.instance.playerFoodPoints = this.food;
	}
	
	// Update is called once per frame
	void Update () {
		//Si no es el turno de PLayer, no ejecutamos
		//el resto de la funcion
		if (!GameManager.instance.playersTurn) {
			return;
		}

		//Indican la direccion en la que se mueve Player
		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)Input.GetAxisRaw ("Horizontal");
		vertical = (int)Input.GetAxisRaw ("Vertical");

		//Impide a Player moverse diagonalmente
		if (horizontal != 0) {
			vertical = 0;
		}

		//Comprobamos si el jugador ha introducido input para moverse
		if (horizontal != 0 || vertical != 0) {
			/*Le pasamos el parámetro genércio Wall, ya que podemos esperar
			que se encuentre con una pared a la hora de moverse e interaccione
			con ella.*/
			AttemptMove<Wall> (horizontal, vertical);
		}

	}


	//Comentario copiado de MovingObject:
	//---
	//Toma un parámetro genérico T.
	//Esta función toma como parámetros un parámetro genérico T,
	// un entero xDir y otro entero yDir.
	//T lo utilizamos para indicar el tipo componente con el que la 
	//unidad interaccionará si es bloqueada.
	//P.ej. en el caso de los enemigos, el tipo de componente
	//con el que interaccionar será el jugador.
	//---
	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		//Cada vez que el Player se mueva, perderá 1HP
		food--;
		//Llamamos a AttempMove() de MovingObject()
		base.AttemptMove <T> (xDir, yDir);

		//Nos permitirá almacenar el resultado del
		//Linecast empleado en Move()
		RaycastHit2D hit;

		CheckIfGameOver ();

		//Final del turno de PLayer
		GameManager.instance.playersTurn = false;
	}

	protected override void OnCantMove <T> (T component)
	{
		//Convierto al parámetro genérico 'component' a Wall
		Wall hitWall = component as Wall;
		//llamo a la funcion damageWall() de Wall para inflingir daño a la pared.
		hitWall.damageWall (wallDamage);
		//Cambio la animacion de Player
		animator.SetTrigger("PlayerChop");
	}

	//Parte de la API de Unity. Lo emplearemos para que Player
	//interaccione con otros objectos como la comida o el EXIT.
	private void OnTriggerEnter2D (Collider2D other)
	{
		//Consultaremo el TAG del objeto con el que se colisiona
		if (other.tag == "Exit") {
			//Llama a la función Restart() con un restraso de 
			//restartLevelDelay segundos.
			Invoke("Restart", restarLevelDelay);
			//"desactivamos" a Player
			enabled = false;
		}
		else if (other.tag == "Food") {
			//Añadimos puntos de comer
			food += pointsPerFood;
			//desactivamos la comida ya que la hemos consumido
			other.gameObject.SetActive (false);
		}
		else if (other.tag == "Soda") {
			//Añadimos puntos de comer
			food += pointsPerSoda;
			//desactivamos la comida ya que la hemos consumido
			other.gameObject.SetActive (false);
		}

	}

	//Cuando Player llegue a Exit, cargaremos el siguiente nivel,
	//que en realidad es el mismo en este caso.
	private void Restart()
	{
		//OBSOLETE. TRY TO UPDATE USING SceneManager.
		Application.LoadLevel(Application.loadedLevel);
	}

	//Utilizado cuando el jugador es atacado.
	public void LoseFood(int loss)
	{
		animator.SetTrigger ("PlayerHit");
		food -= loss;
		CheckIfGameOver();
	}

	void CheckIfGameOver()
	{
		if (this.food <= 0) {
			GameManager.instance.GameOver();
		}
	}
}
