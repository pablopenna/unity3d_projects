using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focusTarget : MonoBehaviour
{

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 directionVector = target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(directionVector);
        }
    }
}
