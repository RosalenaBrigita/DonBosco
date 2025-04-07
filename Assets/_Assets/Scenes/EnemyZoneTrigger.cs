using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using DonBosco.Audio;

public enum ZoneType
{
    Enemy,
    Recruit
}

public class EnemyZoneTrigger2D : MonoBehaviour
{
    [Header("Zone Type")]
    public ZoneType zoneType = ZoneType.Enemy;

    [Header("Enemy or Bola (to be destroyed)")]
    public GameObject[] enemyOrVisuals;

    [Header("Bonus Prajurit")]
    public GameObject[] prajuritToActivate;

    [Header("Delay Setting")]
    public float actionDelay = 1f;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Ally"))
        {
            FriendlyUnit[] allPrajurit = FindObjectsOfType<FriendlyUnit>();
            int totalPrajurit = allPrajurit.Length;

            // Untuk zona musuh: periksa apakah prajurit cukup
            if (zoneType == ZoneType.Enemy && totalPrajurit < enemyOrVisuals.Length)
            {
                Debug.Log("Jumlah prajurit tidak cukup untuk zona musuh.");
                return;
            }

            // Hentikan gerakan prajurit sementara
            foreach (FriendlyUnit unit in allPrajurit)
            {
                unit.canMove = false;
                NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
                if (agent != null) agent.ResetPath();
            }

            // Jalankan aksi berdasarkan tipe zona
            StartCoroutine(ZoneActionCoroutine(allPrajurit));
            triggered = true;
        }
    }

    private IEnumerator ZoneActionCoroutine(FriendlyUnit[] prajuritList)
    {
        if (zoneType == ZoneType.Enemy)
        {
            // Mainkan suara tembakan dan delay tiap musuh
            for (int i = 0; i < enemyOrVisuals.Length; i++)
            {
                if (AudioManager.Instance != null)
                    AudioManager.Instance.Play("22calgun");

                yield return new WaitForSeconds(actionDelay);
            }

            // Hancurkan musuh
            foreach (GameObject enemy in enemyOrVisuals)
            {
                if (enemy != null) Destroy(enemy);
            }
        }
        else if (zoneType == ZoneType.Recruit)
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.Play("collect"); 

            // Animasi bola ke pemain
            foreach (GameObject bola in enemyOrVisuals)
            {
                if (bola != null)
                {
                    StartCoroutine(MoveAndShrink(bola, GetNearestPrajuritPos(prajuritList), actionDelay));
                }
            }

            yield return new WaitForSeconds(actionDelay);

            // Aktifkan prajurit baru
            foreach (GameObject prajurit in prajuritToActivate)
            {
                if (prajurit != null) prajurit.SetActive(true);
            }
        }

        // Hidupkan kembali gerakan
        foreach (FriendlyUnit unit in prajuritList)
        {
            unit.canMove = true;
        }
    }

    private Vector3 GetNearestPrajuritPos(FriendlyUnit[] list)
    {
        Vector3 zonePos = transform.position;
        float minDist = float.MaxValue;
        Vector3 nearest = zonePos;

        foreach (FriendlyUnit unit in list)
        {
            float dist = Vector3.Distance(zonePos, unit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = unit.transform.position;
            }
        }

        return nearest;
    }

    private IEnumerator MoveAndShrink(GameObject obj, Vector3 targetPos, float duration)
    {
        Vector3 startPos = obj.transform.position;
        Vector3 startScale = obj.transform.localScale;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            obj.transform.position = Vector3.Lerp(startPos, targetPos, t);
            obj.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(obj);
    }
}
