using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Adam.DialogueSystem
{
    public class DialogueContent
    {

        public string m_Name;
        [MultiLineProperty(3)]
        public string m_Dialogue;
    }
}

