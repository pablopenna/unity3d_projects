using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System; //to use Serializable
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	//Clase Count para rangos y crear objetos de forma aleatoria
	[Serializable]
	public class Count
	{
		public int min;
		public int max;

		public Count(int min, int max)
		{
			this.min = min;
			this.max = max;
		}
	}

	//variables
	public int columns = 8;
	public int rows = 8;
	//para generar paredes aleatoriamente
	public Count wallCount = new Count(5,8);//minimo 5 paredes, maximo 8
	public Count foodCount = new Count (1,5);
	public GameObject exit;
	public GameObject[] floorTiles; 
	public GameObject[] wallTiles;
	public GameObject[] foodTiles;
	public GameObject[] enemyTiles;
	public GameObject[] outerWallTiles;

	//Para simplificar el arbol de objetos
	//boardHolder será el padre de los GameObjects que vayamos creando.
	//Está sin incializar.
	private Transform boardHolder;
	//lista con todas las casillas del tablero
	private List<Vector3> gridPositions = new List<Vector3>();

	/**llenamos la lista con las posibles posiciones 
	 * donde colocar objetos, paredes, enemigos... 
	 * en el tablero.
	*/
	void initialiseList()
	{
		gridPositions.Clear ();

		//al emepzar en 1 y terminar en columnas o filas - 1 nos aseguramos que los puedan ser transitados.
		for (int x = 1; x < columns - 1; x++) {

			for (int y = 1; y < rows - 1; y++) {
				gridPositions.Add (new Vector3 (x, y, 0f));
			}
		}
	}

	/*Crea el limite del mapa y crea las baldosas del suelo.
	 * quedan por añadir las paredes interiores.
	*/
	void boardSetup()
	{
		//inicializamos boardholder
		boardHolder = new GameObject ("Board").transform;

		//de -1 a +1 para considerar también el borde del mapa
		for (int x = -1; x < columns + 1; x++) {
			for (int y = -1; y < rows + 1; y++) {
				GameObject tileRandom;
				//si me encuentro en el borde del mapa, pongo una pared exterior
				if (x == -1 || x == columns || y == -1 || y == rows) {
					tileRandom = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
				//si no, pongo un tile de suelo cualquiera
				} else {
					tileRandom = floorTiles [Random.Range (0, floorTiles.Length)];
				}
	
				//instanciamos el tile random a GameObject.
				//Instantiate devuelve un Unity.Object, recibiendo otro como parametro
				//tileRandom es como un puntero pues no hemos utilizado new en él.
				//tras saber a donde apunta, creamos un objeto nuevo e independiente => tileRandomInstanciada
				//INSTANTIATE HACE QUE EL GAMEOBJECT APAREZCA EN EL JUEGO
				GameObject tileRandomInstanciada = Instantiate(
					tileRandom, //GameObject a instanciar
					new Vector3 (x,y,0f), //vector del tile (z = 0 pues estamos en 2D)
					//ESTA ES LA POSICION DONDE SE RENDERIZARÄ EL TILE
					Quaternion.identity//sin rotacion
				) as GameObject; //cast a GameObject

				//para tener una jerarquía más ordenada
				tileRandomInstanciada.transform.SetParent(boardHolder); 
		
			}
		}

	}


	//devuelve una posicion aleatoria del tablero de juego
	Vector3 randomPosition(){
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions [randomIndex];
		//para no insertar dos o mas objetos en la misma posicion.
		//eliminamos la posicion en la que hemos insertado de la lista
		//de todas las posiciones del tablero.
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}

	//pone un objeto en una casilla del tablero aleatoria.
	void layoutObjectAtRandom(GameObject[] tileArray, int min, int max)
	{
		//cuantos objectod de una clase vamos a 'spawnear'
		int objectCount = Random.Range (min, max+1);

		for (int i = 0; i < objectCount; i++) {
			Vector3 randomPos = randomPosition ();
			GameObject tile = tileArray [Random.Range (0, tileArray.Length)];
			GameObject instacia = Instantiate (tile, randomPos, Quaternion.identity);

			//de mi cosecha. borrar si sucede algo inesperado
			instacia.transform.SetParent(boardHolder);
		}
	}

	//la unica funcion publica
	public void setupScene(int level)
	{
		boardSetup ();
		initialiseList ();
		layoutObjectAtRandom (wallTiles, wallCount.min, wallCount.max);
		layoutObjectAtRandom (foodTiles, foodCount.min, foodCount.max);
		//funcion logaritmica para crear los enmigos segun el nivel
		// 1 neemigo al lv2, 2 al lv 4, 3 al lv8 ...
		int enemyCount = (int)Mathf.Log(level,2f);
		layoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
		//creamos la salida en su lugar fijo
		GameObject exitInst = Instantiate(exit, new Vector3(columns-1, rows-1, 0F), Quaternion.identity);
		exitInst.transform.SetParent(boardHolder);


	}
		
}
