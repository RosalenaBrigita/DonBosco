using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using DonBosco.Audio;

namespace DonBosco
{
    public class TargetController : MonoBehaviour
    {
        public Button targetButton;
        public Image timerFill;
        public float activeTime = 4f;

        private Action<bool> onFinish;
        public bool IsActive { get; private set; } = false;

        private Coroutine timerRoutine;

        public void ActivateTarget(Action<bool> onComplete)
        {
            if (IsActive) return;

            IsActive = true;
            onFinish = onComplete;

            targetButton.interactable = true;
            timerRoutine = StartCoroutine(TimerCountdown());

            targetButton.onClick.RemoveAllListeners();
            targetButton.onClick.AddListener(OnClick);
        }

        IEnumerator TimerCountdown()
        {
            float t = 0f;
            timerFill.fillAmount = 1f;

            while (t < activeTime)
            {
                t += Time.deltaTime;
                timerFill.fillAmount = 1f - (t / activeTime);
                yield return null;
            }

            Finish(false); // waktu habis
        }

        void OnClick()
        {
            Finish(true);
            AudioManager.Instance.Play("22calgun");
        }

        void Finish(bool success)
        {
            if (!IsActive) return;

            StopCoroutine(timerRoutine);
            IsActive = false;
            targetButton.interactable = false;
            timerFill.fillAmount = 0f;

            // Panggil callback sebelum reset
            onFinish?.Invoke(success);

            // Reset target
            gameObject.SetActive(false);
        }
    }
}
