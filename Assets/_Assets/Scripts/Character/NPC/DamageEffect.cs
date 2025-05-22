using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class DamageEffect : MonoBehaviour
{
    [Header("Flash Settings")]
    [SerializeField] private float flashDuration = 0.15f;
    [SerializeField] private Color flashColor = new Color(1, 0.2f, 0.2f, 1); 
    
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashRoutine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TriggerDamageEffect()
    {
        // Hentikan efek yang sedang berjalan jika ada
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
        
        flashRoutine = null;
    }

    // Reset warna saat nonaktif/destroy
    void OnDisable()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            spriteRenderer.color = originalColor;
        }
    }
}