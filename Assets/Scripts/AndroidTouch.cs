using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AndroidTouch : MonoBehaviour
{
    private float speed = 0.01f;

    [SerializeField]
    Player py;

    //void Update()
    //{
    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
    //    {
    //        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
    //        transform.Translate(0, touchDeltaPosition.y * speed, 0);
    //    }
    //}

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Gamemanager.gameover==false)
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        Vector2 pos = touch.deltaPosition;

                        //if (pos.x <= Gamemanager.bottomLeft.x)
                        //    transform.Translate(Vector3.zero);

                        Vector2 p = transform.position;
                        //Vector2 player = py.transform.position;
                        if (p.x > (Gamemanager.topRight.x - py.radius))
                        {
                            p.x = Gamemanager.topRight.x - py.radius;
                            transform.position = p;
                        }
                        if (p.x < Gamemanager.bottomLeft.x + py.radius)
                        {
                            p.x = Gamemanager.bottomLeft.x + py.radius;
                            transform.position = p;
                        }
                        //if (player.x < Gamemanager.bottomLeft.x)
                        //{
                        //    player.x = Gamemanager.bottomLeft.x;
                        //    transform.position = player;
                        //}
                        //if (player.x > Gamemanager.topRight.x)
                        //{
                        //    player.x = Gamemanager.topRight.x;
                        //    transform.position = player;
                        //}


                        transform.Translate(pos.x * speed, 0, 0);
                    }
                }
            }
        }
    }
    //private void Update()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        if (touch.phase==TouchPhase.Moved)
    //        {
    //            Vector2 pos = touch.position;
    //            transform.Translate(pos.x * speed, 0, 0);
    //        }
    //    }
    //}
}
