using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour {

    UnityEvent HelpEvent;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            HelpEvent.Invoke();
        }
    }
}
