using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Adam.DialogueSystem.Event
{
    [System.Serializable]
    public class DialogueEvent : UnityEvent<string,bool>
    {
    }
}

