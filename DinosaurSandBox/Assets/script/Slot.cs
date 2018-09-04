using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

    public GameObject Prefab,parentd;
    //public GameObject slot;

    public int slotWidth;
    public int slotHigh;
    float X, Y,imagesizex, imagesizey;

    public int NumLED;
    public int LimitPro;

    private List<GameObject> LEDName = new List<GameObject>();

    void Start () {
        StartTile();
    }

    void StartTile()
    {
        //get width , height screen;
        X = Screen.width;
        Y = Screen.height;
        //space  with width,height;
        imagesizex = X / (float)slotWidth;
        imagesizey = Y/ (float)slotHigh;

        
        for (int i = 0; i < slotWidth; i++)
        {
            for (int j = 0; j < slotHigh; j++)
            {
                GameObject Spawn = Instantiate(Prefab);
                Spawn.GetComponent<RectTransform>().sizeDelta = new Vector2(imagesizex *3f, imagesizey*3f);
                Spawn.transform.parent =  parentd.transform.parent;
                Spawn.transform.localScale = new Vector3(1, 1, 1);

                Spawn.AddComponent<BoxCollider>().size = new Vector3(imagesizex*2,imagesizey*2,10f);
                Spawn.GetComponent<BoxCollider>().isTrigger = true;
                        // set Image to devided space            //space//         //middle space// 
                Spawn.transform.position = new Vector3((imagesizex * i) + (imagesizex / 2), (imagesizey * j) + (imagesizey / 2), 0);
            }
        } 
    }

}
