using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    Coin coin;
    [SerializeField]
    PowerUp powerUp;

    int health;
    public static float getCurrentHealth;

    private void Awake()
    {
        health = getMaxHealth();
    }

    public abstract int getMaxHealth();
    public abstract int getCoins();

    void takeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
        getCurrentHealth = Mathf.Clamp(health/1000.0f,0.0f,3f);
        Debug.Log("health=" + health);

        if (health == 0)
            die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            if (bullet.isFromPlayer)
            {
                takeDamage(bullet.getDamage());
                Destroy(collision.gameObject);
            }
        }    
    }


    protected virtual void die()
    {
        int coinCount = getCoins();
        Gamemanager.bossD = false;
        for (int i = 0; i < coinCount; i++)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }

        if (Random.value > 0.9)
        {
            Instantiate(powerUp, transform.position, Quaternion.identity);
        }
        if (gameObject != null)
        {
            Destroy(gameObject);
            //Debug.Log("good things happened :-) " + gameObject);
        }
    }
}
