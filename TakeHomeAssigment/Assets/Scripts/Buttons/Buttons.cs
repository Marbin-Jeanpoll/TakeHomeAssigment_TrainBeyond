using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public static Buttons instance;

    public bool pause;
    [SerializeField] CanvasGroup panelPause, buttonPause, panelFade;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        panelFade.gameObject.SetActive(true);
        LeanTween.alphaCanvas(panelFade, 0, 1).setOnComplete(() =>
        {
            panelFade.gameObject.SetActive(false);
        });
    }

    public void Pause()
    {
        pause = !pause;

        if (pause)
        {
            Time.timeScale = 0;
            panelPause.gameObject.SetActive(true);
            LeanTween.alphaCanvas(panelPause, 1, 0.3f).setIgnoreTimeScale(true);
            LeanTween.alphaCanvas(buttonPause, 0, 0.3f).setIgnoreTimeScale(true);
        }
        else
        {
            Time.timeScale = 1;
            LeanTween.alphaCanvas(panelPause, 0, 0.3f).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                panelPause.gameObject.SetActive(false);
            });
            LeanTween.alphaCanvas(buttonPause, 1, 0.3f).setIgnoreTimeScale(true);
        }
    }

    public void ChangeScene(string level)
    {
        Time.timeScale = 1;
        panelFade.gameObject.SetActive(true);
        LeanTween.alphaCanvas(panelFade, 1, 0.3f).setIgnoreTimeScale(true).setOnComplete(() =>
        {
            SceneManager.LoadScene(level);
        });
    }

    public void QuitGame()
    {
        panelFade.gameObject.SetActive(true);
        LeanTween.alphaCanvas(panelFade, 1, 0.3f).setIgnoreTimeScale(true).setOnComplete(() =>
        {
            //Application.Quit();
            EditorApplication.isPlaying = false;
        });
    }
}
