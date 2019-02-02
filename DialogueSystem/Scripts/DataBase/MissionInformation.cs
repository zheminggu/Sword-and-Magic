using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Adam.DialogueSystem
{
    /// <summary>
    /// need to set npc name when it used
    /// </summary>
    public class MissionInformation
    {
        [HideInInspector]
        public bool m_FinishedMission;

        

        public string m_MissionName=string.Empty;

        [TextArea()]
        public string m_DetailDiscription = string.Empty;

        [TextArea()]
        public string m_ShortDiscription = string.Empty;

        /// <summary>
        /// item people can get when finished this Mission
        /// </summary>
        public List<ItemInfomation> m_Need = new List<ItemInfomation>();

        /// <summary>
        /// item people can get when finished this Mission
        /// </summary>
        public List<ItemInfomation> m_Give=new List<ItemInfomation>();
  
        [Space]
        public bool m_HaveMissionFinishedDialogue;

        
        //[ShowInInspector]//test only
        private string NpcName = string.Empty;

        public void SetMissionNpcName(string _name)
        {
            NpcName = _name;
        }

        public string GetNpcName()
        {
            return NpcName;
        }
        
        public bool MissionComplete()
        {
            foreach (var item in m_Need)
            {
                if (!item.ObtainedEnough())
                {
                    return false;
                }
            }
            return true;
        }

    }
}

