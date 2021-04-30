using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerProjectile"))
        {
            if(transform.root.GetComponent<Animator>()!= null) transform.root.GetComponent<Animator>().SetTrigger("hit");
            gameObject.SetActive(false);
        }
    }
}
