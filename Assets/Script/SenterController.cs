using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro; // Wajib ditambahkan untuk TextMeshPro

public class SenterController : MonoBehaviour
{
    [Header("Referensi Objek")]
    public Transform playerTransform;
    public Light2D senterLight;
    public Slider uiSliderBaterai;
    [Tooltip("Teks UI untuk menampilkan jumlah baterai cadangan.")]
    public TextMeshProUGUI uiTeksCadangan; // UI untuk teks cadangan

    [Header("Pengaturan Baterai")]
    public float maxBaterai = 100f;
    public float kecepatanKuras = 2f;
    [Tooltip("Jumlah baterai cadangan yang dimiliki.")]
    public int cadanganBaterai = 2; // Jumlah cadangan baterai

    // Variabel privat
    private float bateraiSekarang;
    private bool senterAktif = false;

    void Awake()
    {
        bateraiSekarang = maxBaterai;

        if (senterLight != null)
        {
            senterLight.enabled = false;
        }

        if (uiSliderBaterai != null)
        {
            uiSliderBaterai.maxValue = maxBaterai;
            uiSliderBaterai.value = bateraiSekarang;
        }
        
        // Update tampilan UI cadangan baterai di awal
        UpdateTeksCadangan();
    }

    void Update()
    {
        if (senterAktif)
        {
            bateraiSekarang -= kecepatanKuras * Time.deltaTime;

            if (uiSliderBaterai != null)
            {
                uiSliderBaterai.value = bateraiSekarang;
            }

            if (bateraiSekarang <= 0)
            {
                bateraiSekarang = 0;
                MatikanSenter();
            }
        }
    }

    // Method untuk rotasi senter (dari sebelumnya)
    public void OnLook(InputValue value)
    {
        Vector2 mouseScreenPos = value.Get<Vector2>();
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        Vector2 direction = worldMousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (playerTransform.localScale.x < 0)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Method untuk toggle senter (dari sebelumnya)
    public void OnToggleSenter(InputValue value)
    {
        if (value.isPressed)
        {
            ToggleSenter();
        }
    }

    // --- FITUR BARU ---
    // Method ini dipanggil oleh Player Input untuk isi ulang baterai
    public void OnRecharge(InputValue value)
    {
        if (value.isPressed)
        {
            // Cek jika masih punya cadangan DAN baterai tidak penuh
            if (cadanganBaterai > 0 && bateraiSekarang < maxBaterai)
            {
                bateraiSekarang = maxBaterai; // Isi penuh baterai
                
                // -> BARIS INI DITAMBAHKAN UNTUK LANGSUNG UPDATE SLIDER <-
                if (uiSliderBaterai != null)
                {
                    uiSliderBaterai.value = bateraiSekarang;
                }
                
                cadanganBaterai--; // Kurangi satu cadangan
                Debug.Log("Baterai diisi ulang! Sisa cadangan: " + cadanganBaterai);
                
                // Update tampilan UI teks cadangan
                UpdateTeksCadangan();
            }
            else
            {
                Debug.Log("Tidak bisa isi ulang: Baterai cadangan habis atau baterai sudah penuh.");
            }
        }
    }

    // Method untuk mengupdate teks UI cadangan
    private void UpdateTeksCadangan()
    {
        if (uiTeksCadangan != null)
        {
            uiTeksCadangan.text = "" + cadanganBaterai.ToString();
        }
    }
    
    // Fungsi privat (dari sebelumnya)
    private void ToggleSenter()
    {
        if (!senterAktif && bateraiSekarang > 0)
        {
            senterAktif = true;
        }
        else
        {
            senterAktif = false;
        }
        senterLight.enabled = senterAktif;
    }

    private void MatikanSenter()
    {
        senterAktif = false;
        senterLight.enabled = false;
    }
}