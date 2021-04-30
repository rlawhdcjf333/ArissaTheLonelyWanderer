using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour
{
    AudioSource m_audioSource;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.panStereo = -1;

        StartCoroutine(AudioStereoPanning());
    }

    IEnumerator AudioStereoPanning()
    {
        while(true)
        {

            while(m_audioSource.panStereo <1)
            {
                m_audioSource.panStereo += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSecondsRealtime(2f);

            while (m_audioSource.panStereo > -1)
            {
                m_audioSource.panStereo -= Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSecondsRealtime(2f);
        }
    }
}
