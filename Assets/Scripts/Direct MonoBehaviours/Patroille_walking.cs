using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroille_walking : MonoBehaviour
{
    public float distance, speed;
    public bool walking_clockwards, flip_sprite, use_rigidbody;
    private float border_left, border_right;
    private Rigidbody2D rb;

    void Start()
    {
        distance /= 2;
        border_left = transform.position.x - distance;
        border_right = transform.position.x + distance;

        if (flip_sprite)
        {
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }

        if (use_rigidbody)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (transform.position.x < border_left)
        {
            walking_clockwards = true;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
        else if (transform.position.x > border_right)
        {
            walking_clockwards = false;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }

        if (use_rigidbody)
        {
            rb.velocity = new Vector2(walking_clockwards ? speed * 100: -speed * 100, rb.velocity.y);
        }
        else
        {
            transform.Translate(walking_clockwards ? speed : -speed, 0, 0);
        }
    }
}
