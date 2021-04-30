using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpMoth : MonoBehaviour
{
    public void TurnOn()
    {
        gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        EffectManager.instance.CallEffect("LosingMoth", transform.position, Quaternion.identity);
        
        gameObject.SetActive(false);
    }

}
