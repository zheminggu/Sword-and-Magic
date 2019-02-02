using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Adam.DialogueSystem.Event
{
    public enum UIBlock {
        MainBody,
        Tips,
        Ensure,
        Branch,
        All,
        
    }


    [System.Serializable]
    public class MissionFinishedEvent : UnityEvent<string,MissionState>
    {

    }

}
