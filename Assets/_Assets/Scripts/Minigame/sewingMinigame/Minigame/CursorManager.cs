using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D needleCursor; // Gambar kursor jarum
    private Vector2 needleHotspot; // Hotspot di ujung bawah kiri

    void Start()
    {
        // Set hotspot kursor agar sesuai posisi jarum
        needleHotspot = new Vector2(0, needleCursor.height);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Ganti kursor saat masuk ke dalam panel
        Cursor.SetCursor(needleCursor, needleHotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Kembalikan ke kursor default saat keluar
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
