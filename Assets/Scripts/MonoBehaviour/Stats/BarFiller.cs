using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BarFiller : MonoBehaviour
{
    [SerializeField] Image fillableImage;

    void Start()
    {
        fillableImage = GetComponentInChildren<Image>();
        fillableImage.fillAmount = 1;
    }

    public float SetFillAmount(float maxValue, float targetValue)
    {
        //must be positive or zero
        targetValue = Math.Max(targetValue, 0); 
        float res = ToDiscreteFloat(targetValue/maxValue);
        // Debug.Log($"{targetValue} , res: {res}");
        fillableImage.fillAmount = res;
        return res;
    }

    public void SetFull()
    {
        fillableImage.fillAmount = 1f;
    }

    private float ToDiscreteFloat(float value)
    {
        return ((float) Math.Ceiling(value * 100)) / 100;
    }

}
