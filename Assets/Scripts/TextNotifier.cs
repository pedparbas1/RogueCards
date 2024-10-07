using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextNotifier : MonoBehaviour
{
    [SerializeField] float timeToFade = 1f;
    [SerializeField] TMP_Text textContainer;
    [SerializeField] CanvasGroup canvasGroup;
    private Queue<string> queue = new();
    private bool isDisplaying = false;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    public void DisplayNotification(string text)
    {
        if(isDisplaying)
        {
            queue.Enqueue(text);
            return;
        }

        textContainer.text = text;
        StartCoroutine(ShowNotification());
    }

    private IEnumerator ShowNotification()
    {
        isDisplaying = true;
        canvasGroup.alpha = 1;
        yield return new WaitForSeconds(timeToFade);
        canvasGroup.alpha = 0;
        isDisplaying = false;
        OnEndDisplaying();
    }

    private void OnEndDisplaying()
    {
        if(queue.Count > 0) DisplayNotification(queue.Dequeue());
    }
}
