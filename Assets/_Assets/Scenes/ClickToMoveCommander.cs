using UnityEngine;
using UnityEngine.AI;

public class ClickToMoveCommander : MonoBehaviour
{
    [SerializeField] private float spacing = 1.0f; // Jarak antar prajurit
    [SerializeField] private int maxRowSize = 4;   // Maks kolom per baris (biar formasi makin lebar kalau banyak prajurit)
    [SerializeField] private float stoppingDistance = 0.6f;
    [SerializeField] private float agentRadius = 0.3f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            FriendlyUnit[] allPrajurit = FindObjectsOfType<FriendlyUnit>();

            int rowSize = Mathf.Min(maxRowSize, allPrajurit.Length); // Sesuaikan jumlah kolom

            for (int i = 0; i < allPrajurit.Length; i++)
            {
                FriendlyUnit unit = allPrajurit[i];
                if (!unit.canMove) continue; 

                NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    agent.stoppingDistance = stoppingDistance;
                    agent.radius = agentRadius;

                    int row = i / rowSize;
                    int col = i % rowSize;

                    float centerOffsetX = (rowSize - 1) * spacing / 2f;
                    Vector3 offset = new Vector3(col * spacing - centerOffsetX, 0, -row * spacing);
                    Vector3 targetPos = mouseWorldPos + (Vector2)offset;

                    agent.SetDestination(targetPos);
                }
            }
        }
    }

}
