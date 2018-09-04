using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandManager : MonoBehaviour {

    DinoSystemManager DinManager;
    bool isCoverFossil;
	// Use this for initialization
    void Awake()
    {
        DinManager = GameObject.FindObjectOfType<DinoSystemManager>();
    }

    void Start () {
	
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider fossilPart) {
        if (!isCoverFossil && fossilPart.gameObject.layer == 9)
        {
            isCoverFossil = true;
            DinManager.maxCoverFossil ++;
            DinManager.sandCoverFossil ++;
        }
    }

    void OnDestroy()
    {
        if(isCoverFossil)
        {
            DinManager.sandCoverFossil --;
        }
    }

}
