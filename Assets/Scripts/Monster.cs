using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Enemy
{
    // Start is called before the first frame update
    float monsterSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * monsterSpeed);


        if (transform.position.y+transform.localScale.y <= Gamemanager.bottomLeft.y)
        {
            Destroy(gameObject);
        }
        
    }

    public void setSpeed(float speed)
    {
        monsterSpeed = speed;
    }

    public override int getMaxHealth()
    {
        return 100;
    }

    public override int getCoins()
    {
        return 3;
    }
}
