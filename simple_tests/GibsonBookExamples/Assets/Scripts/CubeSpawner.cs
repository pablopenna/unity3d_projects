using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{

    public GameObject myCamera;
    public GameObject cubePrefab;

    private int count;
    // Start is called before the first frame update
    void Start()
    {
        print("Hello:");
        //this y gameObject hacen referencia al GameObject al que este script está vinculado.
        //WRONG: that sentene works for both but there are differences:
        //https://forum.unity.com/threads/this-vs-gameobject-what-are-the-differences.341616/
        //It is better to use gameObject to refer to the GameObject the script is attached to.
        print(gameObject.GetComponent<Transform>().position);

        if(myCamera){
            print("camera specified!");
            //Vector3 objPosition = gameObject.GetComponent<Transform>().position;
            //Vector3 cameraPosition = myCamera.GetComponent<Transform>().position;
            Vector3 objPosition = gameObject.transform.position;
            Vector3 cameraPosition = myCamera.transform.position;

            print(objPosition);

            /*objPosition.x = cameraPosition.x;
            objPosition.y = cameraPosition.y;
            objPosition.z = cameraPosition.z + 10;*/

            //gameObject.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, cameraPosition.z + 10);
            
            print(objPosition);

            print("true:" + gameObject.GetComponent<Transform>().position);
        }
        else{
            print("no camera specified!");
        }

        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        count++;

        var newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
        transform.position = newPos;

        if(count % 30 == 0){
            var newCube = Instantiate<GameObject>(cubePrefab);
            newCube.transform.position = newPos;
        }
    }
}
