using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Adam.DialogueSystem
{
    public class DatabaseManager : SerializedMonoBehaviour
    {
        [Tooltip ("If npc have dialogue at start you should set it")]
        public DialogueData m_CurrentDialogueData;

        public Dictionary<string ,DialogueData> m_DialogueDatas=new Dictionary<string, DialogueData>();

       
        public DialogueData GetDialogueData()
        {
            if (m_CurrentDialogueData!=null)
            {
                return Instantiate(m_CurrentDialogueData);
            }
            else
            {
                return null;
            }

        }

        public DialogueData GetDialogueData(string _missionName)
        {
            DialogueData _dialogueData=new DialogueData();
            foreach (var item in m_DialogueDatas)
            {
                if (item.Value.m_Mission.m_MissionName.Equals(_missionName))
                {
                    _dialogueData = Instantiate(item.Value);
                    _dialogueData.m_Mission.m_FinishedMission = true;
                    _dialogueData.m_Mission.SetMissionNpcName(gameObject.transform.parent.name);
                    break;
                } 
            }
           
            return _dialogueData;
        }

        public void SetDialogueData(string _dialogueName)
        {
            m_CurrentDialogueData = m_DialogueDatas[_dialogueName];
        }

    }
}

