using UnityEngine;
using DonBosco.Dialogue;

public class ObjectActivateFromInk : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private string inkVariableName = "set_bendera";
    [SerializeField] private float checkInterval = 0.3f; // Interval pengecekan (optimasi performa)

    private bool lastState;
    private float timer;

    private void Start()
    {
        if (objectToActivate == null)
        {
            Debug.LogError("Object to activate is not assigned!", this);
            enabled = false; // Nonaktifkan script jika object null
            return;
        }

        CheckAndSetInitialState();
        lastState = objectToActivate.activeSelf;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            CheckInkVariable();
        }
    }

    private void CheckInkVariable()
    {
        var dialogueManager = DialogueManager.GetInstance();
        if (dialogueManager == null) return;

        if (dialogueManager.GetVariableState(inkVariableName) is Ink.Runtime.BoolValue boolVal)
        {
            bool newState = boolVal.value;
            if (newState != lastState)
            {
                objectToActivate.SetActive(newState);
                lastState = newState;
                Debug.Log($"{inkVariableName} changed to: {newState}");
            }
        }
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

        if (dialogueManager.GetVariableState(inkVariableName) is Ink.Runtime.BoolValue boolVal)
        {
            objectToActivate.SetActive(boolVal.value);
            lastState = boolVal.value;
            Debug.Log($"Initial state set to: {boolVal.value}");
        }
    }
}