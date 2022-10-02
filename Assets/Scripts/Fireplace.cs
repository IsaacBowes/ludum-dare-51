using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fireplace : MonoBehaviour
{
    public Color targetColor;
    public Color targetColor1;
    public Light2D lightToChange;
    void Start()
    {
        lightToChange = gameObject.GetComponent<Light2D>();
        StartCoroutine(LerpFunction(targetColor, .1f));
    }
    IEnumerator LerpFunction(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = lightToChange.color;
        while (time < duration)
        {
            lightToChange.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        lightToChange.color = endValue;
    }
    IEnumerator LerpBack(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = lightToChange.color;
        while (time < duration)
        {
            lightToChange.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        lightToChange.color = endValue;
    }
    private void Update()
    {
        float rand = Random.Range(.1f, 2.5f);


        if(lightToChange.color == targetColor)
        {
            StartCoroutine(LerpBack(targetColor1, rand));
        }
        if (lightToChange.color == targetColor1)
        {
            StartCoroutine(LerpBack(targetColor, rand));
        }

    }
}
