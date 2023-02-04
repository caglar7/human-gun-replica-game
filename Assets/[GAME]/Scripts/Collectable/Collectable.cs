using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    bool isUsed = false;
    Gun gun;

    private void OnTriggerEnter(Collider other)
    {
        gun = other.GetComponent<Gun>();
        if(gun && isUsed == false)
        {
            isUsed = true;
            OnCollect();
        }
    }

    public virtual void OnCollect()
    {
        Debug.Log("OnCollect() not implemented");
    }
}
