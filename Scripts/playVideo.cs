using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class playVideo : MonoBehaviour {

    public VideoPlayer m_Video;
    private void Start()
    {
        m_Video.Play();
    }
    private void Update()
    {
        Debug.Log(m_Video.isPlaying);
    }
}
