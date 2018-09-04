using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControll : MonoBehaviour {

    public void DestroyitSelf()
    {
        Debug.Log(this);
        Destroy(this.gameObject);
    }
}
