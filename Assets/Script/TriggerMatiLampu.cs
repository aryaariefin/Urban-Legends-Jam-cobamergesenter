using System.Collections; // Wajib ditambahkan untuk menggunakan Coroutine
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TriggerMatiLampu : MonoBehaviour
{
    [Header("Referensi Cahaya")]
    [Tooltip("Hubungkan komponen Global Light 2D dari scene ke sini.")]
    public Light2D globalLight;

    [Header("Pengaturan Intensitas")]
    [Tooltip("Intensitas cahaya saat pemain di dalam zona trigger.")]
    public float intensitasSaatMasuk = 0f;
    [Tooltip("Intensitas cahaya normal saat pemain di luar zona.")]
    public float intensitasNormal = 1f;
    [Tooltip("Waktu dalam detik sebelum cahaya kembali normal.")]
    public float durasiGelap = 300f; // 5 menit = 300 detik

    private bool sudahTerpicu = false; // Mencegah trigger terpanggil berkali-kali

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah yang masuk adalah Player dan trigger belum pernah terpicu
        if (other.CompareTag("Player") && !sudahTerpicu)
        {
            sudahTerpicu = true; // Tandai bahwa trigger sudah aktif

            if (globalLight != null)
            {
                
                globalLight.intensity = intensitasSaatMasuk;
                Debug.Log("Pemain masuk zona, cahaya diredupkan.");

                
                StartCoroutine(KembalikanCahayaSetelahDelay());
            }

            
            transform.position = new Vector3(10000f, 10000f, 0f); 
        }
    }

    // Coroutine untuk menangani jeda waktu
    private IEnumerator KembalikanCahayaSetelahDelay()
    {
        // Tunggu selama durasi yang ditentukan (misal: 300 detik)
        yield return new WaitForSeconds(durasiGelap);

        // Setelah menunggu, kembalikan intensitas cahaya ke normal
        if (globalLight != null)
        {
            globalLight.intensity = intensitasNormal;
            Debug.Log("5 menit berlalu, cahaya kembali normal.");
        }
    }
}