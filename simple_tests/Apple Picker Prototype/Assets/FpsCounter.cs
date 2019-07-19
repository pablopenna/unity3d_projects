using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    private Text UIText;

    // Start is called before the first frame update
    void Start()
    {
        UIText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float fps = 1.0f/Time.deltaTime;
        UIText.text = fps.ToString();
    }
}
