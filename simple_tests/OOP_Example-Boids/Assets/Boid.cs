using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    [Header("Set Dinamically")]
    public Rigidbody rigid;
    private Neighbourhood neighbourhood;

    public Vector3 pos{
        get{
            return transform.position;
        }
        set{
            transform.position = value;
        }
    }

    //initialization
    private void Awake() {
        neighbourhood = GetComponent<Neighbourhood>();
        rigid = GetComponent<Rigidbody>();
        //set random initial position
        pos = Random.insideUnitSphere * Spawner.S.spawnRadius;
        //random initial velocity
        Vector3 vel= Random.onUnitSphere * Spawner.S.velocity;
        rigid.velocity = vel;

        LookAhead();

        //Give random color to the boid
        Color randColor = Color.black;
        while(randColor.r + randColor.g + randColor.b < 1.0f){
            randColor = new Color(Random.value, Random.value, Random.value);
        }

        Renderer[] rands = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rands){
            r.material.color = randColor;
        }

        TrailRenderer tRend = GetComponent<TrailRenderer>();
        tRend.material.SetColor("_TintColor", randColor);

    }

    void LookAhead(){
        //Orients boid to look forward
        transform.LookAt(pos + rigid.velocity);
    }

    private void FixedUpdate() {
        Vector3 vel = rigid.velocity;
        Spawner spn = Spawner.S;

        //COLLISION AVOIDANCE - avoid neighbours which are tooclose
        Vector3 velAvoid = Vector3.zero;
        Vector3 tooClosePos = neighbourhood.avgClosePos;
        if(tooClosePos != Vector3.zero){
            velAvoid = pos - tooClosePos;
            velAvoid.Normalize();
            velAvoid *= spn.velocity;
        }

        //VECLOCITY MATCHING
        Vector3 velAlign = neighbourhood.avgVel;
        if(velAlign != Vector3.zero){
            velAlign.Normalize();
            velAlign*=spn.velocity;
        }

        //FLOCKCENTERING
        Vector3 velCenter = neighbourhood.avgPos;
        if(velCenter != Vector3.zero){
            velCenter -= transform.position;
            velCenter.Normalize();
            velCenter *= spn.velocity;
        }
        


        //ATTRATION - Move towards the attractor
        Vector3 delta = Attractor.POS - pos;
        //check if we are attracted or repelled by the attractor
        bool attracted = (delta.magnitude > spn.attractPushDist);
        Vector3 velAttract = delta.normalized * spn.velocity;

        //Apply all velocities
        float fdt = Time.fixedDeltaTime;
        
        if(velAvoid != Vector3.zero){
            vel = Vector3.Lerp(vel, velAvoid, spn.collAvoid*fdt);
        }
        else{
            if(velAlign != Vector3.zero){
                vel = Vector3.Lerp(vel, velAlign, spn.velMatching*fdt);
            }
            if(velCenter != Vector3.zero){
                //vel = Vector3.Lerp(vel, velAlign, spn.flockCentering*fdt);
                vel = Vector3.Lerp(vel, velCenter, spn.flockCentering*fdt);
            }
            if(velAttract != Vector3.zero){
                if(attracted) {
                    vel = Vector3.Lerp(vel, velAttract, spn.attractPull * fdt);
                }
                else{
                    vel = Vector3.Lerp(vel, -velAttract, spn.attractPush * fdt);
                }
            }
        }


        

        //Set vel to the velocity set on the Spawner singleton
        vel = vel.normalized * spn.velocity;
        //assign it to the rigidbody
        rigid.velocity = vel;
        //Look in the direction of the new velocitu
        LookAhead();
    }
}
