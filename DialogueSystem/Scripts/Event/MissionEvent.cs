using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Adam.DialogueSystem.Event
{
    /// <summary>
    /// the state of Npc if it has a mission to player
    /// </summary>
    public enum MissionState {
        HasMission,
        DuringMisson,
        FinishedMission,
        NoneMission,
    }

    [System.Serializable]
    public class MissionEvent : UnityEvent<string,string>
    {
       
    }

 

}
