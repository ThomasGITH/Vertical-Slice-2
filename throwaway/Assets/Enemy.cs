using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void Callback(string message);
    Callback callback = Hud.Callback;

    void Start()
    {
        Die(callback);
    }

    public void Die(Callback callback)
    {
        callback("im dead");
    }
}
