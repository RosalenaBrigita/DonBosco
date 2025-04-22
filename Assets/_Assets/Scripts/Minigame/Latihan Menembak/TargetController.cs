using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

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
            Finish(true); // klik berhasil
        }

        void Finish(bool success)
        {
            if (!IsActive) return;

            if (timerRoutine != null)
                StopCoroutine(timerRoutine);

            IsActive = false;
            targetButton.interactable = false;
            timerFill.fillAmount = 0f;

            onFinish?.Invoke(success);
            gameObject.SetActive(false);
        }
    }

}
