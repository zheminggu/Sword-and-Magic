using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using Adam.DialogueSystem.Event;

namespace  Adam.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        [ShowInInspector]
        private static List<GameObject> NPCs=new List<GameObject>();

        private  DialogueEventManager dialogueEventManager;

        private  AdamUIController UIController;

        // Use this for initialization
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Debug.Log("already have a DialogeManager, automatic delete extra");
                Destroy(gameObject);
            }
   
            //InitializeNPCs();
            dialogueEventManager = GetComponent<DialogueEventManager>();
            UIController = GetComponent<AdamUIController>();
        }

      
        #region utils
        public static DialogueManager GetDialogueManager()
        {
            return Instance;
        }

     
        public DialogueEventManager GetDialogueEventManager()
        {
            return Instance.dialogueEventManager;
        }


        public AdamUIController GetUIController()
        {
            return Instance.UIController;
        }

        public static List<GameObject> GetNPCs()
        {
            return NPCs;
        }



        #endregion

        private void InitializeNPCs()
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("AdamNPC"))
            {
                NPCs.Add(item);
            }
            
        }

    }

}
