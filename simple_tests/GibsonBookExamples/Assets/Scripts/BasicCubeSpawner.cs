using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCubeSpawner : MonoBehaviour
{

    public GameObject cubePrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpellItOut();
        Instantiate(cubePrefab);
    }

    void SpellItOut()
    {
        string sA = "Hello World";
        string sB = "";

        for (int i = 0; i < sA.Length; i++)
        {
            sB += sA[i];
        }

        print(sB);
    }

}