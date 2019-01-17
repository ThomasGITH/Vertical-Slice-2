using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    static uint points;

    private void Start()
    {
        
    }

    public static void Callback(string message)
    {
        MonoBehaviour.print(message);
        points++;
    }
}