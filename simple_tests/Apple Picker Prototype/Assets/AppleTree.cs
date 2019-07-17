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

    //CUSTOM
    //screen width converted to world size
    private Vector3 worldWidth;
    //tree width
    private float treeWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        childrenRenderers = GetComponentsInChildren<Renderer>();
        //Get width that the tree will traverse- Camera.main returns the camera instance tagged as main camera
        worldWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        treeWidth = GetTreeWidth();
        leftAndRightEdge = worldWidth.x-treeWidth;
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
            //speed *= -1;
        }

        //DEBUG
        //print("debug:"+transform.position.x);
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

    float GetTreeWidth(){
        float maxWidth=0f;
        foreach(Renderer rend in childrenRenderers){
            if(rend.bounds.size.x > maxWidth){
                maxWidth = rend.bounds.size.x;
            }
        }
        return maxWidth;
    }
}
