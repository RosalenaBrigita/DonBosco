using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DonBosco
{
    public class AllyZoneTrigger : MonoBehaviour
    {
        [Header("Ally Trigger Enter")]
        [SerializeField] private bool disableInputMoveOnEnter = true;
        [SerializeField] private UnityEvent onAllyEnter = null;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ally"))
            {
                if (disableInputMoveOnEnter)
                {
                    InputManager.Instance.SetMovementActionMap(false);
                }
                onAllyEnter?.Invoke();
            }
        }
    }
}

