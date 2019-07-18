using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in INspector")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;

    // Start is called before the first frame update
    void Start()
    {
        basketList = new List<GameObject>();
        //Create Basket
        for(int i=0;i<numBaskets;i++){
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AppleDestroyed(){
        //Destroy All of the falling apples
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach(GameObject apple in tAppleArray){
            Destroy(apple);
        }

        this.DestroyBasket();
    }

    public void DestroyBasket(){
        int basketIndex = basketList.Count-1;
        if(basketIndex >= 0){
            GameObject dyingBasket = basketList[basketIndex];
            basketList.RemoveAt(basketIndex);
            Destroy(dyingBasket);
        }else{
            //Restart
            this.RestartGame();
        }
    }

    public void RestartGame(){
        SceneManager.LoadScene("_Scene_0");
    }
}
