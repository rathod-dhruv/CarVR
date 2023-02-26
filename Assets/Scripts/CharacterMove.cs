using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public bool x, y, z;
    public float speed = 0.01f;
    void Update()
    {
        Vector3 pos = transform.localPosition;
        if(x)
            pos.x += speed;
        else if(y)
            pos.y += speed;
        else if(z)
            pos.z += speed;

        transform.localPosition = pos;
    }
}
