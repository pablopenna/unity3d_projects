using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

	//variables públicas
	//Tiempo que le llevará moverse al objeto
	public float moveTime = 0.1f;
	//Capa con la que el objeto comprobará colisiones
	public LayerMask blockingLayer;

	//variables privadas
	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2D;
	private float inverseMoveTime;


	// Use this for initialization
	//las funciones protected virtual pueden ser sobrescritas por sus hijos.
	/*ABSTRACT VS VIRTUAL
	-> ABSTRACT OBLIGA a los hijo a hacer override (a implementar la funcionalidad ya que en
	las funciones abstract no tienen codigo a ejecutar).
	-> VIRTUAL ofrece una funcionalidad ya hecha que puede ser utilizada por el hijo o sobreescrita
	por el mismo a su elección. Permite utilizar su funcionalidad o sobreescribirla.
	*/
	protected virtual void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();
		rb2D = GetComponent<Rigidbody2D> ();
		//utilizaremos inverseMoveTime para realizar los
		//cálculos sobre moveTime multiplicando en lugar
		//de dividiendo, ya que multiplicar es más 
		//eficiente computacionalmente.
		inverseMoveTime = 1f / moveTime;
		
	}

	//con 'out' pasamos el argumento por referencia (para retornar más de un valor con la función).
	/*Función utilizada para implementar el movimiento*/
	protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
	{
		//conversion implicita de vector3d a vector2d
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		//desactivamos nuestro collider de forma que cuando
		//'casteemos' nuestro Ray no hagamos colisión
		//con nosotros mismos.
		boxCollider.enabled = false;
		//casteamos una linea desde start hasta end comprobando
		//colisiones con la blocking layer.
		hit = Physics2D.Linecast (start, end, blockingLayer);
		//volvemos a activar nuestro boxCollider
		boxCollider.enabled = true;
		 
		//si hit.transform == null significa que no se ha encontrado
		//ninguna colision en el Ray que hemos casteado, por lo que procederemos 
		//a mover el objeto
		if (hit.transform == null) {
			//Creamos un hilo?
			StartCoroutine(SmoothMovement (end) );
			//indicamos que nos hemos movido
			return true;
		}

		//Indicamos que se ha detectado colisión y no nos hemos movido
		return false; 
	}
		

	//Para mover el objeto. EL argumento vector3 lo utilizaremos para saber
	//a dónde nos movemos.
	/*Función utilizada para obtener los puntos que compondrán la línea de movimiento del objeto.
	En cada iteración de la función obtenemos el siguiente punto que compone la linea recta que
	representa el movimiento a realizar.*/
	protected IEnumerator SmoothMovement (Vector3 end)
	{
		//distancia restante a moverse
		//utilizamos sqrMagnitude en vez de magnitude porque es más eficiente
		//computacionalmente
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		//float.Epsilon es un número muy pequeño, casi cero.
		while (sqrRemainingDistance > float.Epsilon) {
			//vector3.MoveTowards mueve un punto en linea recta hacia el objetivo
			//calculamos el siguiente punto a movernos
			Vector3 newPosition = Vector3.MoveTowards (rb2D.position, end, inverseMoveTime * Time.deltaTime);
			//actualizamos la posicion delegate rigidbofy con EffectorSelection2D punto
			rb2D.MovePosition (newPosition);
			//actualizamos la distancia restante
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;

			//hace que se espere un frame más antes de reevaluar la condicion del bucle
			//Supongo que esto es debido el IEnumerator (es una interfaz de colecciones)
			yield return null;
		}
	}

	//Toma un parámetro genérico T.
	//Esta función toma como parámetros un parámetro genérico T,
	// un entero xDir y otro entero yDir.
	//T lo utilizamos para indicar el tipo componente con el que la 
	//unidad interaccionará si es bloqueada.
	//P.ej. en el caso de los enemigos, el tipo de componente
	//con el que interaccionar será el jugador.
	protected virtual void AttemptMove <T>(int xDir, int yDir)
		where T:Component
	{
		RaycastHit2D hit;
		//Tenemos que pasarle el tercer argumento
		//a Move por referencia.
		//De forma que si durante move se detecta colision
		//nuestra variable hit no será null
		bool canMove = Move (xDir, yDir, out hit);

		//Si no hay colisiones en el movimiento no hacemos nada
		if (hit.transform == null)
			return;

		//Si si ha habido colisiones...

		//obetenmos el componente con el que se ha colisionado
		T hitComponent = hit.transform.GetComponent<T>();

		//Si no se ha podido mover el objeto y
		//este ha colisionado con algo
		if (!canMove && hitComponent != null) {
			//Llamamos a la funcion para interaccionar con el 
			//objeto con el que hemos colisionado
			OnCantMove(hitComponent);
		}

	
	}



	//La declaración se hace incompleta. Las clases que heredan
	//sobreescribiran esta función.
	protected abstract void OnCantMove<T> (T Component)
		where T : Component;

}
