using UnityEngine;
using System.Threading.Tasks;
using DonBosco.SaveSystem;

public class QuitPrevention : MonoBehaviour
{
    public GameObject quitConfirmPanel;
    private bool isQuitting = false;
    private bool isProgrammaticQuit = false; // Flag baru

    void Awake()
    {
        Application.wantsToQuit += WantsToQuit;
    }

    bool WantsToQuit()
    {
        // Langsung izinkan jika quit dari script
        if (isProgrammaticQuit) return true;

        // Tampilkan panel jika user-initiated quit
        if (!isQuitting)
        {
            ShowQuitPanel();
            return false;
        }
        return true;
    }

    // Untuk quit dari UI/Alt+F4
    void ShowQuitPanel()
    {
        if (quitConfirmPanel != null)
        {
            quitConfirmPanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            ForceQuit();
        }
    }

    // Untuk quit dari script (tanpa konfirmasi)
    public void RequestImmediateQuit()
    {
        isProgrammaticQuit = true;
        isQuitting = true;
        ForceQuit();
    }

    // Untuk quit dengan konfirmasi (dari button UI)
    public async void ConfirmQuit()
    {
        isQuitting = true;
        try
        {
            if (SaveManager.Instance != null)
                await SaveManager.Instance.SaveGame();
        }
        finally
        {
            ForceQuit();
        }
    }

    void ForceQuit()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void CancelQuit()
    {
        isQuitting = false;
        if (quitConfirmPanel != null)
        {
            quitConfirmPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void OnDestroy()
    {
        Application.wantsToQuit -= WantsToQuit;
    }
}