using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector!")]
    public GameObject applePrefab;
    //Movement Speed
    public float speed = 1f;
    //Distance where AppleTree turns around
    public float leftAndRightEdge = 20f;
    public float chanceToChangeDirections=0.1f;
    //Apples instantiatio rate
    public float secsBetweenAppleDrops = 1f;

    private Renderer[] childrenRenderers;
    
    // Start is called before the first frame update
    void Start()
    {
        childrenRenderers = GetComponentsInChildren<Renderer>();;
        //Drop an apple after 2 seconds
        Invoke("DropApple", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        //Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        //Change direction
        if(pos.x > leftAndRightEdge){
            //Going out of right border => go left
            speed = -Mathf.Abs(speed);
        }
        else if(pos.x < -leftAndRightEdge)
        {
            //Going out of left border => go right
            speed = Mathf.Abs(speed);
        }
        //Change direction randomly
        else if( Random.value < chanceToChangeDirections){
            speed *= -1;
        }
    }

    void DropApple(){
        GameObject apple = Instantiate<GameObject>(applePrefab);
        //apple.transform.position = transform.position;
        apple.transform.position = GetAppleTreeCenterPos();
        //Drop another apple in x seconds
        Invoke("DropApple", secsBetweenAppleDrops);

    }

    Vector3 GetAppleTreeCenterPos(){
        Renderer[] rendererList = childrenRenderers;
        Vector3 centerPos = new Vector3(0,0,0);
        int counter = 0;
        foreach(Renderer rend in rendererList){
            centerPos += rend.bounds.center;
            counter++;
        }
        if(counter > 0){
            centerPos /= counter;
        }
        return centerPos;
    }
}
