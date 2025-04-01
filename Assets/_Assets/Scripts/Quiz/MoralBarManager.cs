using UnityEngine;
using UnityEngine.UI;

public class MoralBarManager : MonoBehaviour
{
    public static MoralBarManager Instance { get; private set; }

    [SerializeField] private Slider moralBar;
    [SerializeField] private Image fillArea; // Tambahkan referensi ke Fill Area dari Slider

    private int currentMoral = 50; // Awalnya 50 (netral)

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateMoral(0); // Memastikan warna sesuai dengan nilai awal
    }

    public void UpdateMoral(int value)
    {
        currentMoral = Mathf.Clamp(currentMoral + value, 0, 100);
        moralBar.value = currentMoral;

        UpdateMoralBarColor();
    }

    private void UpdateMoralBarColor()
    {
        if (currentMoral >= 80)
        {
            fillArea.color = Color.green; // Moral tinggi (Hijau)
        }
        else if (currentMoral < 50)
        {
            fillArea.color = Color.red; // Moral rendah (Merah)
        }
        else
        {
            fillArea.color = Color.yellow; // Moral sedang (Kuning)
        }
    }

    public int GetMoral()
    {
        return currentMoral;
        Debug.Log(currentMoral);
    }
}
