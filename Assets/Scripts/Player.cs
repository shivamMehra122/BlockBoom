using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Transform spawnSpot;

    [SerializeField]
    Bullet bulletPrefab;

    Color color;

    public float radius;
    float speed = 10f;


    public delegate void GainCoin();
    public event GainCoin onGainCoin;

    public delegate void PlayerDied();
    public event PlayerDied onPlayerDied;

    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;

        //radius = transform.localScale.x/2;
        radius = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        //shootBullet();
        InvokeRepeating("shootBullet", 0.5f, 0.1f);

    }

    // Update is called once per frame
    void Update()
    {
        //PC
        float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed ;

        if (transform.position.x + radius >= Gamemanager.topRight.x && moveHorizontal > 0)
            moveHorizontal = 0;
        if (transform.position.x - radius <= Gamemanager.bottomLeft.x && moveHorizontal < 0)
            moveHorizontal = 0;
        transform.Translate(moveHorizontal * Vector3.right);

        //Android
        Vector2 player = transform.position;
        if (player.x < Gamemanager.bottomLeft.x+radius)
        {
            player.x = Gamemanager.bottomLeft.x+radius;
            transform.position = player;
        }
        if (player.x > Gamemanager.topRight.x-radius)
        {
            player.x = Gamemanager.topRight.x-radius;
            transform.position = player;
        }

    }

    void shootBullet()
    {
        Bullet bulletGO = Instantiate(bulletPrefab);
        //bulletGO.transform.SetParent(spawnSpot);
        bulletGO.transform.position = spawnSpot.position;
        //bulletGO.Init(5f, GetComponent<SpriteRenderer>().color, 0.25f);
        bulletGO.Init(Vector2.up, 10f, color, 0.25f, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            if (onGainCoin != null)
            {
                onGainCoin();
            }
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "Enemy")
        {
            if (onPlayerDied != null)
            {
                onPlayerDied();
            }
        }
        else if (collision.transform.tag == "Bullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            if (!bullet.isFromPlayer)
            {
                if (onPlayerDied != null)
                {
                    onPlayerDied();
                }
                Destroy(collision.gameObject);
            }
        }
        else if (collision.transform.tag == "PowerUp")
        {
            Debug.Log("PowerUp Received");
            Destroy(collision.gameObject);
        }
        
    }
}
