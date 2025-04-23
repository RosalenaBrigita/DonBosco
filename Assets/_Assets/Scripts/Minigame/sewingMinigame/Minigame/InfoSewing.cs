using UnityEngine;

public class InfoSewing : MonoBehaviour
{
    public GameObject infoPanel;

    public void HideInfo()
    {
        infoPanel.SetActive(false);
    }
    public void ShowInfo()
    {
        infoPanel.SetActive(true);
    }
}
