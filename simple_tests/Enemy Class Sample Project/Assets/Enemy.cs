using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f; //speed in m/s
    public float fireRate = 0.3f; //Shots/second
    //This is a Property: A method that acts like a field
    public Vector3 pos {
        get { return this.transform.position; }
        set { this.transform.position = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public virtual void Move(){
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision coll) {
        GameObject other = coll.gameObject;
        switch(other.tag){
            case "Hero":
                //Code to destroy hero
                break;
            case "HeroLaser":
                //Destroy this enemy
                Destroy(this.gameObject);
                break;
        }
    }

}
