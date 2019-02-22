using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Vector2 localscale;
    // Start is called before the first frame update
    void Start()
    {
        localscale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //localscale.x = Enemy.getCurrentHealth;
        //Debug.Log(Enemy.getCurrentHealth);
        localscale.x = Enemy.getCurrentHealth;
        transform.localScale = localscale;

    }
}
