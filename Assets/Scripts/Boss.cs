using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    Transform bulletSpawnSpot;

    [SerializeField]
    Transform bulletSpawnSpotNew;

    [SerializeField]
    Bullet bulletPrefab;

    Color color;
    Transform player;

    public delegate void bossDied();
    public static event bossDied onBossDied;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        color = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        if (Gamemanager.bossD)
        {
            Debug.Log("BossDDDDDDD " + Gamemanager.bossD);
            InvokeRepeating("ShootPlayer", 1.5f, 2f);
        }
        else
        {
            Debug.Log("BossDDDDDDD " + Gamemanager.bossD);
            InvokeRepeating("ShootPlayer", 1.5f, Random.Range(1f, 2f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, Time.deltaTime*4f);   
    }

    void ShootPlayer()
    {
        Vector2 dir = (player.position - bulletSpawnSpot.transform.position).normalized;

        Bullet bulletGo = Instantiate(bulletPrefab, bulletSpawnSpot.position, Quaternion.identity) as Bullet;
        bulletGo.Init(dir, Random.Range(1f,4f), color, 0.4f, false);

        //second boss
        if (Gamemanager.bossD)
        {
        Vector2 dirB = (player.position - bulletSpawnSpotNew.transform.position).normalized;
        bulletGo = Instantiate(bulletPrefab, bulletSpawnSpotNew.position, Quaternion.identity) as Bullet;
        bulletGo.Init(dirB, Random.Range(1f,4f), color, 0.4f, false);
        }
    }

    protected override void die()
    {
        if (onBossDied != null)
        {
            onBossDied();
        }
        base.die();
    }

    public override int getMaxHealth()
    {
        if (Gamemanager.bossD)
        {
            //Gamemanager.bossD = false;
            return 2500;
        }
        else
        {
            return 2000;
        }
    }

    public override int getCoins()
    {
        if (Gamemanager.bossD)
        {
            return 15;
        }
        else
        {
            return 10;
        }
    }
}
