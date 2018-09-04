using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DinoSystemManager : MonoBehaviour {

    [SerializeField]
    GameObject boxPoint;
    [SerializeField]
    LayerMask layerSand;
    [SerializeField]
    LayerMask layerFossil;
    [SerializeField]
    GameObject particleSmoke;
    [SerializeField]
    Transform ParentPaticle;


    public int maxCoverFossil { get; set;}
    [Range(0f,1f)]
    public float percentToComplete;
    public int sandCoverFossil{ get; set;}
    bool isCompleteDig;
    [SerializeField]
    GameObject textComplete;

    [SerializeField]
    Text testTextComplete;

    [SerializeField]
    Text textTimeCount; 
    public float timeCount  = 20f;
    [SerializeField]
    Button buttonRestart;


    [SerializeField]
    GameObject sand;
    [SerializeField]
    Transform startPoint;
    [SerializeField]
    int xSand,ySand;

    int lengthX = 0;
    int lengthY = 0;

    [SerializeField]
    GameObject textDialog;
    [SerializeField]
    Transform alphaChildDialog;

    void Start () {
        //sandGenerate(-30);
    }

    void Update () {

        Debug.Log("start :" + (sandCoverFossil * 100) / maxCoverFossil);
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        boxPoint.transform.position = new Vector3(Input.mousePosition.x,Input.mousePosition.y,-100f);

        Debug.DrawRay(boxPoint.transform.position,boxPoint.transform.forward*100f,Color.red);
        RemoveSand();
        CheckDigging();

        testTextComplete.text = "all is :" + ((sandCoverFossil * 100)/maxCoverFossil + "." + (sandCoverFossil * 100)%maxCoverFossil + " % ");

        Debug.Log("have completed : " + ((sandCoverFossil * 100) / maxCoverFossil) /100);
    }
            
    #region Method Erease/remove sand
    void RemoveSand()
    {
        RaycastHit hitOBJ;
        if (Input.GetMouseButton(0) && Physics.Raycast(boxPoint.transform.position,boxPoint.transform.forward,out hitOBJ,Mathf.Infinity,layerSand))
        {
            Debug.Log("hit sandUI");
            GameObject dust = Instantiate(particleSmoke, hitOBJ.transform.position + (hitOBJ.transform.forward * -20f), particleSmoke.transform.rotation);
            dust.transform.SetParent(ParentPaticle);
            dust.transform.position = Input.mousePosition;

            Image imageSand =  hitOBJ.transform.GetComponent<Image>();
            Image imageChild = hitOBJ.transform.GetChild(0).GetComponent<Image>();

            float alphaSand = imageSand.color.a;
            float alphaChildSand = imageChild.color.a;

            float randomForceDig = alphaSand - 0.2f;

            imageSand.color = new Color(imageSand.color.r, imageSand.color.g,imageSand.color.b, randomForceDig);
            imageChild.color  = new Color(imageChild.color.r, imageChild.color.g, imageChild.color.b,  randomForceDig);

            if (alphaSand <= 0 || alphaChildSand <= 0)
            {
                Destroy(hitOBJ.transform.gameObject);
            }
            //destroy sand on fossile Point
            if (hitOBJ.transform.name == "fossilPoint")
            {
                //fossil.RemoveAt(0);
            }
            //imagePaticle.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
    #endregion

    #region checkListFossil
    void CheckDigging()
    {       
        //if dig less than "percentComplete" will complete          
        if ( ((sandCoverFossil * 100f) / maxCoverFossil) /100 <= percentToComplete )
        {
            Debug.Log("complete Dig show detail");
            Debug.Log("sandCoverFossil" + sandCoverFossil);
            Debug.Log("max Fossil" + maxCoverFossil);
            Debug.Log("percent have to Complete " + percentToComplete);
            Debug.Log("completed :" + ((sandCoverFossil * 100) / maxCoverFossil));

            //textComplete.SetActive(true);
            //do function
            ShowDialog();
        }
    }
    #endregion

    #region newCode [Use to set rotation as a ray's obj hit]
    // from https://www.youtube.com/watch?v=fFq5So-UB0E
    void Example() {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.forward);
    }

    void ShowDialog()
    {
        textDialog.transform.DOScale(1f, 1f);
        //show time
        textTimeCount.gameObject.SetActive(true);
        timeCount -= (int)1f * Time.deltaTime;
        textTimeCount.text =  "watch other dinosaur :" +((int)timeCount).ToString();
        if (timeCount <= 0)
        {
            buttonRestart.transform.DOScale(1f, 1f);
            timeCount = 0;
        }

        //show dialog
        foreach(Transform allAlphaChild in alphaChildDialog)
        {
            Image alphaChild = allAlphaChild.GetComponent<Image>();
            Text alphaChildOfChild = allAlphaChild.GetChild(0).GetComponent<Text>();

            alphaChild.DOFade(1f, 1f);
            alphaChildOfChild.DOFade(1f, 1f);
        }
    }
    #endregion
}
