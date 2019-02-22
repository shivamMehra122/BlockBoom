using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
