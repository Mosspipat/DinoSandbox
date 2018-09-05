using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandManager : MonoBehaviour {

    [SerializeField]
    GameObject particleSmoke;
    [SerializeField]
    Transform ParentPaticle;

    DinoSystemManager DinManager;
    bool isCoverFossil;
	// Use this for initialization
    void Awake()
    {
        DinManager = GameObject.FindObjectOfType<DinoSystemManager>();
        ParentPaticle = GameObject.Find("particleZone").transform;
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

    public void EaseOutSand() {
        Debug.Log("spawn dust");
        GameObject dust = Instantiate(particleSmoke, Input.mousePosition , particleSmoke.transform.rotation);
        dust.transform.SetParent(ParentPaticle);
        dust.transform.position = Input.mousePosition;

        Image imageSand = GetComponent<Image>();
        Image imageChild = transform.GetChild(0).GetComponent<Image>();

        float alphaSand = imageSand.color.a;
        float alphaChildSand = imageChild.color.a;

        float randomForceDig = alphaSand - 0.2f;

        imageSand.color = new Color(imageSand.color.r, imageSand.color.g, imageSand.color.b, randomForceDig);
        imageChild.color = new Color(imageChild.color.r, imageChild.color.g, imageChild.color.b, randomForceDig);

        if (alphaSand <= 0 || alphaChildSand <= 0) {
            Destroy(transform.gameObject);
        }
    }
}
