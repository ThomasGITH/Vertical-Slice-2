using UnityEngine;
using System;

public class item_pickup : MonoBehaviour
{
    public bool FinalItem;
    public static event Action pickup, final_item;
    private bool sizeDown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !sizeDown)
        {
            sizeDown = true;
            if (pickup != null)
            {
                pickup();
            }
            else
            {
                print("No classes subscribed to event.");
            }
        }
    }

    private void Update()
    {
        if(sizeDown)
        {
            transform.localScale += FinalItem ? transform.localScale / 10 : -(transform.localScale / 10);
            if (transform.localScale.x <= 0.01f) { Destroy(gameObject); }
            if (FinalItem) { final_item?.Invoke(); }
        }
    }
}
