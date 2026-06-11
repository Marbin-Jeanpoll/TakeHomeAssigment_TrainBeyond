using System.Collections;
using UnityEngine;

public class ScrewSequence : MonoBehaviour
{
    public static ScrewSequence Instance;

    [SerializeField] PutBolt[] putBolts;
    [SerializeField] int index = 0;

    [SerializeField] CanvasGroup panelFinish, buttonPause;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        putBolts[index].ActivateEmission();
    }

    public void NextBolt()
    {
        if(index < putBolts.Length-1)
        {
            index++;
            putBolts[index].ActivateEmission();
        }
        else
        {
            panelFinish.gameObject.SetActive(true);
            LeanTween.alphaCanvas(panelFinish, 1, 0.3f).setIgnoreTimeScale(true);
            LeanTween.alphaCanvas(buttonPause, 0, 0.3f).setIgnoreTimeScale(true);
        }
    }
}
