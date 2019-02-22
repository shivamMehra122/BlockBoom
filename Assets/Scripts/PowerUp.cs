using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float xForce = Random.Range(-50f, 50f);
        float yForce = Random.Range(50f, 150f);
        Vector2 force = new Vector2(xForce, yForce);

        rb.AddForce(force);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y + transform.localScale.y <= Gamemanager.bottomLeft.y)
        {
            Destroy(gameObject);
        }
    }
}
