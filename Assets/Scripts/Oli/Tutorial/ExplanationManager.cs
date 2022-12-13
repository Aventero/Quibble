using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplanationManager : MonoBehaviour
{
    public static event UnityAction OnExplanationTrigger;
    public static event UnityAction OnExplanationSecondaryTrigger;

    public List<Explanation> explanations;
    public float timeBetweenExplanations;

    [Header("GUI References")]
    public GameObject explanationBox;
    public TMPro.TMP_Text explanationText;

    private Explanation current;
    private Queue<string> explanationsQueue;

    private void Start()
    {
        explanationsQueue = new Queue<string>();
    }

    public void StartExplanation(int index)
    {
        // Load explanation
        current = explanations[index];

        // Load explanation sentences
        explanationsQueue.Clear();
        foreach (string part in current.parts)
        {
            explanationsQueue.Enqueue(part);
        }

        explanationBox.SetActive(true);

        // Display first sentence
        DisplayNextPart();
    }

    public void DisplayNextPart()
    {
        // If end of explanantion is reached => End explanantion
        if (explanationsQueue.Count == 0)
        {
            EndExplanation();
            return;
        }

        string nextPart = explanationsQueue.Dequeue();

        // Check if this part is trigger
        if (nextPart.Equals("<TRIGGER>"))
        {
            OnExplanationTrigger.Invoke();
            DisplayNextPart();
        }
        else if (nextPart.Equals("<SECONDARYTRIGGER>"))
        {
            OnExplanationSecondaryTrigger.Invoke();
            DisplayNextPart();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(nextPart));
        }
    }

    public void EndExplanation()
    {
        explanationBox.SetActive(false);

        current = null;
    }

    IEnumerator TypeSentence(string sentence)
    {
        explanationText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            explanationText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }

        // Show next part after delay
        yield return new WaitForSeconds(timeBetweenExplanations);

        DisplayNextPart();
    }

    public IEnumerator StartExplanation(int index, float time)
    {
        yield return new WaitForSeconds(time);
        StartExplanation(index);
    }

    public static void ResetTrigger()
    {
        if (OnExplanationTrigger != null)
        {
            foreach (Delegate invoker in OnExplanationTrigger.GetInvocationList())
                OnExplanationTrigger -= (UnityAction)invoker;
        }

        if (OnExplanationSecondaryTrigger != null)
        {
            foreach (Delegate invoker in OnExplanationSecondaryTrigger.GetInvocationList())
                OnExplanationSecondaryTrigger -= (UnityAction)invoker;
        }
    }
}
