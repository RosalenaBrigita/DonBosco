using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DonBosco.Dialogue;

public class ObjectActivateFromInk : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate; 
    [SerializeField] private string inkVariableName = "set_bendera";

    private void Start()
    {
        if (objectToActivate == null)
        {
            Debug.LogError("Object to activate is not assigned!", this);
            return;
        }

        CheckAndSetInitialState();
    }

    private void CheckAndSetInitialState()
    {
        var dialogueManager = DialogueManager.GetInstance();
        if (dialogueManager == null)
        {
            Debug.LogWarning("DialogueManager not found, using default inactive state");
            objectToActivate.SetActive(false);
            return;
        }

        if (dialogueManager.GetVariableState(inkVariableName) is Ink.Runtime.BoolValue boolVal && boolVal.value)
        {
            objectToActivate.SetActive(boolVal.value);
            Debug.Log($"Set {objectToActivate.name} active state to: {boolVal.value} (based on Ink variable)");
        }
        else
        {
            Debug.LogWarning($"Ink variable '{inkVariableName}' not found or not a boolean, using default inactive state");
            objectToActivate.SetActive(false);
        }
    }
}
