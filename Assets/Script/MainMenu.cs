using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject panel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Scene()
    {
     SceneManager.LoadScene("testing 2");
    }
   public void ExitApp()
    {
        Application.Quit();
    }
   public void PanelOn()
    {
        if (panel == null)
        {
            Debug.Log("Panel Tidak Ada");
        }
        else
        {
            panel.SetActive(true);
        }
    }
   public void PanelOff()
    {
        if (panel == null)
        {
            Debug.Log("Panel Tidak Ada");
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
