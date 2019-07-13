using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighbourhood : MonoBehaviour
{
    [Header("Set dinamically")]
    public List<Boid> neighbours;
    private SphereCollider coll;
    public Vector3 avgPos{
        get{
            Vector3 avg = Vector3.zero;
            if(neighbours.Count == 0){
                return avg;
            }
            for(int i=0;i<neighbours.Count;i++){
                avg+=neighbours[i].pos;
            }
            avg /= neighbours.Count;
            return avg;
        }
    }
    public Vector3 avgVel{
        get{
            Vector3 avg = Vector3.zero;
            if(neighbours.Count == 0){
                return avg;
            }
            for(int i=0;i<neighbours.Count;i++){
                avg+=neighbours[i].rigid.velocity;
            }
            avg /= neighbours.Count;
            return avg;
        }
    }
    public Vector3 avgClosePos {
        get{
            Vector3 avg = Vector3.zero;
            Vector3 delta;
            int nearCount = 0;
            for(int i=0; i<neighbours.Count;i++){
                delta = neighbours[i].pos - transform.position;
                if(delta.magnitude <= Spawner.S.collDist){
                    avg += neighbours[i].pos;
                    nearCount++;
                }
            }
            if(nearCount == 0){
                return avg;
            }
            avg /= nearCount;
            return avg;
        }
    }

    void Start()
    {
        neighbours = new List<Boid>();
        coll = GetComponent<SphereCollider>();
        coll.radius = Spawner.S.neighbourDist/2;
    }

    void FixedUpdate()
    {
        if(coll.radius != Spawner.S.neighbourDist/2){
            coll.radius = Spawner.S.neighbourDist/2;
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        Boid b = other.GetComponent<Boid>();
        if(b != null){
            if(neighbours.IndexOf(b) == -1){
                neighbours.Add(b);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        Boid b = other.GetComponent<Boid>();
        if(b != null){
            if(neighbours.IndexOf(b) != -1){
                neighbours.Remove(b);
            }
        }
    }
}
