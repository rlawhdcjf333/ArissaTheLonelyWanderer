using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    GameObject m_curtainPrefab;
    void Start()    
    {
        m_curtainPrefab = Resources.Load<GameObject>("Prefab/Curtain");
        GameObject curtain = Instantiate(m_curtainPrefab, transform);
        Destroy(curtain, 2.1f);

        if(transform.Find("VillageName") != null)
        StartCoroutine(CoroutineVillageName());
    }

    IEnumerator CoroutineVillageName()
    {
        yield return new WaitForSeconds(1.8f);
        transform.Find("VillageName").gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        transform.Find("VillageName").gameObject.SetActive(false);
    }

}
