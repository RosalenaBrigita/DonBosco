using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FinalScoreDisplay : MonoBehaviour
{
    public Image PerfectImage;
    public Image GoodImage;
    public Image BadImage;

    void Start()
    {
        // Sembunyikan semua gambar di awal
        PerfectImage.gameObject.SetActive(false);
        GoodImage.gameObject.SetActive(false);
        BadImage.gameObject.SetActive(false);
    }

    public void ShowFinalScore(string result)
    {
        Image selectedImage = null; // Variabel untuk menyimpan image yang aktif

        // Tentukan image yang akan ditampilkan berdasarkan hasil
        if (result == "Perfect")
        {
            selectedImage = PerfectImage;
        }
        else if (result == "Good")
        {
            selectedImage = GoodImage;
        }
        else if (result == "Bad")
        {
            selectedImage = BadImage;
        }

        if (selectedImage == null) return; // Jika tidak ada yang sesuai, keluar dari fungsi

        // Munculkan image yang sesuai
        selectedImage.gameObject.SetActive(true);
        selectedImage.transform.localScale = Vector3.zero; // Reset ukuran sebelum animasi

        // Animasi sesuai hasil
        if (result == "Perfect")
        {
            selectedImage.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack)
                .OnComplete(() => HideAfterDelay(selectedImage));
        }
        else if (result == "Good")
        {
            selectedImage.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBounce)
                .OnComplete(() => HideAfterDelay(selectedImage));
        }
        else if (result == "Bad")
        {
            selectedImage.transform.DOScale(1f, 0.3f).SetEase(Ease.InOutElastic)
                .OnComplete(() =>
                {
                    selectedImage.transform.DOShakePosition(0.5f, 10f, 10, 90, false, true)
                        .OnComplete(() => HideAfterDelay(selectedImage));
                });
        }
    }

    void HideAfterDelay(Image image)
    {
        DOVirtual.DelayedCall(1.5f, () =>
        {
            image.gameObject.SetActive(false); // Hilangkan setelah 1.5 detik
        });
    }
}
