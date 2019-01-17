using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loop : MonoBehaviour
{
    public GameObject dragdude;

    bool lewp;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            lewp = true;
            dragdude.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        if(lewp)
        {
            dragdude.transform.Rotate(0,0,2.0f);
            dragdude.transform.Translate(0.2f,0,0);
        }
    }
}
