using UnityEngine;
using CodeMonkey.Utils;

public class HowToBulletTracer : MonoBehaviour
{
    [SerializeField] private GameObject muzzleFlashPrefab; // Prefab muzzle flash
    [SerializeField] private Transform gunEndPoint; // Posisi ujung senjata

    public void Shoot(Vector3 gunEndPointPosition, Vector3 shootPosition)
    {
        CreateShootFlash(gunEndPointPosition);
        ShakeCamera(.5f, .05f);
    }

    private void CreateShootFlash(Vector3 spawnPosition)
    {
        // Spawn muzzle flash di posisi senjata
        GameObject flash = Instantiate(muzzleFlashPrefab, spawnPosition, Quaternion.identity);

        // Sesuaikan agar efek tetap berada di layer yang benar
        flash.transform.SetParent(gunEndPoint);
        flash.transform.localRotation = Quaternion.identity;

        // Hapus efek setelah 0.1 detik
        Destroy(flash, 0.1f);
    }

    public static void ShakeCamera(float intensity, float timer)
    {
        Vector3 lastCameraMovement = Vector3.zero;
        FunctionUpdater.Create(delegate ()
        {
            timer -= Time.unscaledDeltaTime;
            Vector3 randomMovement = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized * intensity;
            Camera.main.transform.position = Camera.main.transform.position - lastCameraMovement + randomMovement;
            lastCameraMovement = randomMovement;
            return timer <= 0f;
        }, "CAMERA_SHAKE");
    }
}
