using System.Collections;
using UnityEngine;

public class PutBolt : MonoBehaviour
{
    [SerializeField] bool canTouch;
    bool once;
    [SerializeField] GameObject bolt;
    [SerializeField] float offsetMax = 0.2f;
    [SerializeField] float offsetMin = 0.2f;
    Renderer rend;
    [SerializeField] Color redColor = Color.red;
    Coroutine corutineChangeEmission;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void ActivateEmission()
    {
        canTouch = true;
        corutineChangeEmission = StartCoroutine(ChangeEmission());
    }

    private void OnMouseDown()
    {
        if (!once)
        {
            if (canTouch)
            {
                once=true;  

                if (corutineChangeEmission != null)
                {
                    StopCoroutine(corutineChangeEmission);
                }

                ChangeEmission(0);

                LeanTween.moveY(bolt, transform.position.y + offsetMax, .8f).setOnComplete(() =>
                {
                    LeanTween.rotate(bolt, new Vector3(90, 0, 180), .9f).setDelay(0.1f);
                    LeanTween.moveY(bolt, transform.position.y - offsetMin, 1f).setEase(LeanTweenType.easeInSine).setOnComplete(() =>
                    {
                        ScrewSequence.Instance.NextBolt();
                        this.enabled = false;
                    });
                });
            }
            else
            {
                ShakeCamera.Instance.CameraMove(1, .1f, 1);

                StartCoroutine(ChangeColor());
            }
        }
    }

    IEnumerator ChangeColor()
    {
        ChangeColor(redColor);
        yield return new WaitForSeconds(0.6f);
        ChangeColor(Color.white);
    }

    public void ChangeColor(Color cTarget)
    {
        Color startColor = rend.material.color;

        LeanTween.value(gameObject, startColor, cTarget, .5f).setOnUpdate((Color val) =>
        {
            rend.material.color = val;
        }).setEase(LeanTweenType.easeInOutQuad);
    }

    IEnumerator ChangeEmission()
    {
        ChangeEmission(1);
        yield return new WaitForSeconds(0.6f);
        ChangeEmission(0);
        yield return new WaitForSeconds(0.6f);
        yield return ChangeEmission();
    }

    public void ChangeEmission(float targetIntensity)
    {
        Color currentEmission = rend.material.GetColor("_EmissionColor");
        float startIntensity = currentEmission.maxColorComponent;

        rend.material.EnableKeyword("_EMISSION");

        LeanTween.value(gameObject, startIntensity, targetIntensity, 0.5f).setOnUpdate((float val) =>
        {
            Color emissionColor = rend.material.color * val;
            rend.material.SetColor("_EmissionColor", emissionColor);
        }).setEase(LeanTweenType.easeInOutQuad);
    }
}
