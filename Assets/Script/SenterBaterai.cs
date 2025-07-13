using UnityEngine;
using UnityEngine.InputSystem; // Wajib untuk Input System baru
using UnityEngine.UI; // Wajib untuk mengakses komponen UI seperti Slider
using UnityEngine.Rendering.Universal;

public class SenterBaterai : MonoBehaviour
{
    [Header("Pengaturan Senter")]
    [Tooltip("Komponen Light 2D atau Light yang akan digunakan sebagai senter.")]
    public Light2D senterLight; 
    [Header("Pengaturan Baterai")]
    [Tooltip("Kapasitas maksimal baterai.")]
    public float maxBaterai = 100f;
    [Tooltip("Kecepatan baterai terkuras per detik saat senter menyala.")]
    public float kecepatanKuras = 2f;

    // Variabel privat untuk melacak status
    private float bateraiSekarang;
    private bool senterAktif = false;

    [Header("Pengaturan UI")]
    [Tooltip("Slider UI untuk menampilkan sisa baterai.")]
    public Slider uiSliderBaterai;

    void Start()
    {
        // Inisialisasi baterai
        bateraiSekarang = maxBaterai;

        // Pastikan senter mati di awal
        if (senterLight != null)
        {
            senterLight.enabled = false;
        }

        // Atur UI Slider
        if (uiSliderBaterai != null)
        {
            uiSliderBaterai.maxValue = maxBaterai;
            uiSliderBaterai.value = bateraiSekarang;
        }
    }

    void Update()
    {
        // Jika senter aktif dan masih ada baterai
        if (senterAktif && bateraiSekarang > 0)
        {
            // Kurangi baterai seiring waktu
            bateraiSekarang -= kecepatanKuras * Time.deltaTime;

            // Jika baterai habis, matikan senter
            if (bateraiSekarang <= 0)
            {
                bateraiSekarang = 0; // Jangan sampai minus
                MatikanSenter();
            }
        }

        // Update UI secara terus-menerus
        if (uiSliderBaterai != null)
        {
            uiSliderBaterai.value = bateraiSekarang;
        }
    }

    // Method ini akan dipanggil oleh Player Input Component
    public void OnToggleSenter(InputAction.CallbackContext context)
    {
        // Hanya jalankan saat tombol benar-benar ditekan (bukan dilepas)
        if (context.performed)
        {
            // Jika senter sedang mati dan baterai masih ada, nyalakan
            if (!senterAktif && bateraiSekarang > 0)
            {
                NyalaMatikanSenter();
            }
            // Jika senter sedang menyala, matikan
            else if (senterAktif)
            {
                NyalaMatikanSenter();
            }
        }
    }

    private void NyalaMatikanSenter()
    {
        senterAktif = !senterAktif;
        senterLight.enabled = senterAktif;
    }

    private void MatikanSenter()
    {
        senterAktif = false;
        senterLight.enabled = false;
    }
}