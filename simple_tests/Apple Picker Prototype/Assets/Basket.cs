using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Enables the use of uGUI features

public class Basket : MonoBehaviour
{

    public float speed = 30f;
    public float moveMargin = 5f;
    private Text scoreGT;

    void Start(){
        //moveMargin = this.GetComponent<Renderer>().bounds.size.x * 0.5f;

        //Get reference to ScoreCounter
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        //Get the Text component of said GO
        scoreGT = scoreGO.GetComponent<Text>();
        //Initial points = 0
        scoreGT.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        //get mouse position
        
        Vector3 mousePos2D = Input.mousePosition;
        //The camera's Z postion sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;
        //convert point from 2d screen space into 3d game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //Move x position of the basket into the x position of the mouse
        /* Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos; */

        Vector3 pos = this.transform.position;

        Vector3 targetVector = mousePos3D - pos;

        if(targetVector.x > moveMargin){
            pos.x += speed * Time.deltaTime ;    
        }else if(targetVector.x < -moveMargin){
            pos.x -= speed * Time.deltaTime ;    
        }

        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll){
        GameObject collidedWith = coll.gameObject;
        if(collidedWith.tag == "Apple"){
            Destroy(collidedWith);

            //score
            int score = int.Parse(scoreGT.text);
            score += 100;
            scoreGT.text = score.ToString();

            //max score
            if(score > MaxScore.maxScore){
                MaxScore.maxScore = score;
            }
        }
    }
}
