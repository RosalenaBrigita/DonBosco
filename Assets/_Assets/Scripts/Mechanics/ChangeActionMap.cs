using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    public class ChangeActionMap : MonoBehaviour
    {
        // Start is called before the first frame update
        public void EnableMovement()
        {
            InputManager.Instance.SetMovementActionMap(true);
        }
        public void DisableMovement()
        {
            InputManager.Instance.SetMovementActionMap(false);
        }
    }
}
