using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioSource playerShoot;
    public AudioSource bossShoot;

    Vector2 dir;
    float speed;

    public bool isFromPlayer;

    private void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        playerShoot = audios[0];
        bossShoot = audios[1];


        audiosound();
    }

    // Start is called before the first frame update
    public void Init(Vector2 myDir, float mySpeed, Color mycolor, float scale, bool _isFromPlayer)
    {
        dir = myDir;
        speed = mySpeed;  

        //transform.localPosition = Vector2.zero;

        GetComponent<SpriteRenderer>().color = mycolor;
        isFromPlayer = _isFromPlayer;

        transform.localScale *= scale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);

        if (transform.position.y <= Gamemanager.bottomLeft.y || transform.position.y>=Gamemanager.topRight.y)
        {
            Destroy(gameObject);
        }
        
    }

    public int getDamage()
    {
        return 30;
    }

    void audiosound()
    {
        if (isFromPlayer)
        {
            playerShoot.Play();
        }
        else
        {
            bossShoot.Play();
        }

    }
}
