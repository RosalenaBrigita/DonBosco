using UnityEngine;
using Cinemachine;

public class MinigameCameraSwitcher : MonoBehaviour
{
    [Header("Camera Settings")]
    public CinemachineVirtualCamera cmMinigameCamera;
    public int cameraPriority = 50; // pastikan lebih tinggi dari kamera lain

    void Start()
    {
        if (cmMinigameCamera != null)
        {
            cmMinigameCamera.Priority = cameraPriority;
        }
        else
        {
            Debug.LogWarning("Minigame camera belum di-assign!");
        }
    }
}
