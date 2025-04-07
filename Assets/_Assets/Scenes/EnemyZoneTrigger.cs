using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyZoneTrigger2D : MonoBehaviour
{
    [Header("Enemy Data")]
    public int enemyCount = 2;
    public GameObject[] enemyToDestroy;

    [Header("Bonus Prajurit")]
    public int bonusPrajurit = 2;
    public GameObject[] prajuritToActivate;

    [Header("Delay Setting")]
    public float destroyDelay = 2f;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        Debug.Log("Trigger entered by: " + other.name);

        if (other.gameObject.layer == LayerMask.NameToLayer("Ally"))
        {
            FriendlyUnit[] allPrajurit = FindObjectsOfType<FriendlyUnit>();
            int totalPrajurit = allPrajurit.Length;

            if (totalPrajurit >= enemyCount)
            {
                // Hentikan gerakan dulu
                foreach (FriendlyUnit unit in allPrajurit)
                {
                    unit.canMove = false;
                    NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
                    if (agent != null) agent.ResetPath();
                }

                // Aktifkan tambahan prajurit
                foreach (GameObject prajurit in prajuritToActivate)
                {
                    if (prajurit != null) prajurit.SetActive(true);
                }

                // Mulai proses hancurkan musuh dan hidupkan kembali gerakan
                StartCoroutine(DestroyEnemiesThenReactivate(allPrajurit));

                triggered = true;
            }
        }
    }

    private IEnumerator DestroyEnemiesThenReactivate(FriendlyUnit[] prajuritList)
    {
        yield return new WaitForSeconds(destroyDelay);

        // Hancurkan semua musuh
        foreach (GameObject enemy in enemyToDestroy)
        {
            if (enemy != null) Destroy(enemy);
        }

        // Aktifkan kembali gerakan prajurit
        foreach (FriendlyUnit unit in prajuritList)
        {
            unit.canMove = true;
        }
    }
}
