using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adam.DialogueSystem
{
    public class SceneChangeActive : MonoBehaviour
    {
        public GameObject m_ChangeScene;
        public string m_DialogueName;
        public void SetChangeSceneActive()
        {

            if (gameObject.GetComponent<DatabaseManager>().m_CurrentDialogueData)
            {
                //Debug.Log(gameObject.GetComponent<DatabaseManager>().m_CurrentDialogueData.name);
                if (gameObject.GetComponent<DatabaseManager>().m_CurrentDialogueData.name.Equals(m_DialogueName))
                {
                    m_ChangeScene.SetActive(true);
                }
            }

        }
    }
}
