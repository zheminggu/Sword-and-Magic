using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Adam.DialogueSystem
{
    [CreateAssetMenu(menuName = "AdamDialogue/DialogueData")]
    public class DialogueData : SerializedScriptableObject
    {
        [Title("Dialogues")]
 
        public List<DialogueContent> m_Dialogues=new List<DialogueContent>();
        
        

        #region MissionData
        [Title("Mission")]
        public bool IsAMission;

        [ShowIf("IsAMission")]
        public MissionInformation m_Mission;

        [InfoBox("add if choose HaveFinishedMissionDialogue")]
        public List<DialogueContent> m_FinishedMissionDialogues=new List<DialogueContent>();

        private string dialoguePersonName;
        #endregion

     
        [Title("After this Dialogue Finished")]
        public List<NameAndMission> m_SetNpcDialogue=new List<NameAndMission>();

        private bool haveNextDialogue=true;
        private int currentDialogueIndex = 0;

        public bool HaveNextDialogue()
        {
            return haveNextDialogue;
        }

        public string GetNextDialogue()
        {
            string _tempString="";
     
            if ( IsAMission&& m_Mission.m_FinishedMission&&m_Mission.m_HaveMissionFinishedDialogue)
            {
                if (haveNextDialogue)
                {
                    _tempString = m_FinishedMissionDialogues[currentDialogueIndex].m_Dialogue;
                    dialoguePersonName = m_FinishedMissionDialogues[currentDialogueIndex].m_Name;
                    currentDialogueIndex++;
                    if (currentDialogueIndex == m_FinishedMissionDialogues.Count)
                    {
                        haveNextDialogue = false;
                    }
                }
            }
            else if (IsAMission&& m_Mission.m_FinishedMission && !m_Mission.m_HaveMissionFinishedDialogue)
            {
                haveNextDialogue = false;
            }
            else
            {
                if (haveNextDialogue)
                {
                    _tempString = m_Dialogues[currentDialogueIndex].m_Dialogue;
                    dialoguePersonName = m_Dialogues[currentDialogueIndex].m_Name;
                    currentDialogueIndex++;
                    if (currentDialogueIndex == m_Dialogues.Count)
                    {
                        haveNextDialogue = false;
                    }
                }
            }
            
         
            return _tempString;
        }
        public string GetDialoguingPersonName()
        {
            return dialoguePersonName;
        }
        public MissionInformation GetMission()
        {
            return m_Mission ;
        }
    }

}
