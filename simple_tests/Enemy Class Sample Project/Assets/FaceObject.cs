using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceObject : MonoBehaviour
{
    public GameObject target;
    private GameObject myCamera;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null){
            //Get vector from camera to target
            Vector3 focusVector = target.transform.position - myCamera.transform.position;
            Quaternion focusRotation = Quaternion.LookRotation(focusVector);
            myCamera.transform.rotation = focusRotation;
        }
    }
}
