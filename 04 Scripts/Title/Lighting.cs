using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(transform.up, Time.deltaTime * 60f);   
    }
}
