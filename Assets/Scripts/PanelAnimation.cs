using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnimation : MonoBehaviour
{
    public float time;

    private void OnEnable()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        StartCoroutine(panel());
    }

    IEnumerator panel()
    {
        while (transform.localScale.x<1 && transform.localScale.y<1 && transform.localScale.z<1)
        {
            transform.localScale = new Vector3(transform.localScale.x + time * Time.deltaTime, transform.localScale.y + time * Time.deltaTime, transform.localScale.z + time * Time.deltaTime);
            yield return null;
        }
        transform.localScale= Vector3.one;
        Time.timeScale = 0;
    }

}
