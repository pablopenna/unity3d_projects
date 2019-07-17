using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float yBottom = -15f;
    //CUSTOM - get game window size
    private float worldBottom ;

    // Start is called before the first frame update
    void Start()
    {
        worldBottom = transform.position.y - Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0.0f)).y;
    }

    // Update is called once per frame
    void Update()
    {
        //print("bottom:"+worldBottom);
        if(transform.position.y < yBottom){
            Destroy(this.gameObject);
        }
    }
}
