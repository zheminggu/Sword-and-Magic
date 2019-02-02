using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Adam.DialogueSystem.Event;

public class OnButtonClicked : MonoBehaviour {
    
    public MissionChangeEvent m_MissionChangeEvent;
    public string m_ItemName;
    public int m_ItemNumber;

    public void OnClicked()
    {
        m_MissionChangeEvent.Invoke(m_ItemName, m_ItemNumber);
    }

 
}
