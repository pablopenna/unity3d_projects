using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{

    public GameObject myCamera;
    public GameObject cubePrefab;
    //scale factor to be applied to the cubes for resizing them
    public float scaleFactor;
    //when a cubes scale is smaller than the threshold, delete the cube
    public float deleteScaleThreshold;


    private int count;
    private List<GameObject> cubeList;
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
            //print(objPosition);
            //Place object at same height as camera
            gameObject.transform.position = new Vector3(objPosition.x, cameraPosition.y, objPosition.z);            
            //print(objPosition);
            //print("true:" + gameObject.GetComponent<Transform>().position);
        }
        else{
            print("no camera specified!");
        }
        this.count = 0;
        this.cubeList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        this.count++;
        FocusCameraOnObject();

        Vector3 spawnerPos = MoveGameObjectForward();
        if(this.count % 30 == 0){
            SpawnCube(spawnerPos);
            ReduceCubesInList();
            GiveCubesRandomColor();
            DestroySmallCubes();
        }
    }

    void FocusCameraOnObject()
    {
        if (myCamera)
        {
            Vector3 posVector = transform.position - myCamera.transform.position;
            myCamera.transform.rotation = Quaternion.LookRotation(posVector);

        }

    }

    Vector3 MoveGameObjectForward()
    {
        var newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
        transform.position = newPos;
        return newPos;
    }

    void SpawnCube(Vector3 spawnPosition)
    {
        var newCube = Instantiate<GameObject>(cubePrefab);
        newCube.transform.position = spawnPosition;
        //add to list
        this.cubeList.Add(newCube);
    }

    void ReduceCubesInList()
    {
        float scale;
        foreach (GameObject cube in this.cubeList)
        {
            //print("BEGIN:" + cube.transform.localScale);

            //good approach
            scale = cube.transform.localScale.x;
            //print("scale:" + scale);
            scale *= this.scaleFactor;
            //print("converted scale:" + scale);
            //Vector3 newScale = new Vector3(scale, scale, scale);
            Vector3 newScale = Vector3.one * scale;
            //print("final scale:" + newScale);
            cube.transform.localScale = newScale;

            //bad approach?
            //cube.transform.localScale = cube.transform.localScale * 1.001f;

            //print("END:" + cube.transform.localScale);
        }
    }

    void GiveCubesRandomColor()
    {
        Color color;
        foreach (GameObject cube in this.cubeList)
        {
            color = new Color(Random.value, Random.value, Random.value);
            cube.GetComponent<Renderer>().material.color = color;
        }
    }

    void DestroySmallCubes()
    {
        print("Tamaño Lista:" + this.cubeList.Count);
        List<GameObject> tempCubeList = new List<GameObject>(this.cubeList);
        foreach (GameObject cube in tempCubeList)
        {
            var scale = cube.transform.localScale.x;
            if (scale < this.deleteScaleThreshold)
            {
                this.cubeList.Remove(cube);
                Destroy(cube);
            }
        }
    }
}
