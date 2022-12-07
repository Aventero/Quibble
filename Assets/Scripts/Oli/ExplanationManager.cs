using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationManager : MonoBehaviour
{
    public List<Explanation> explanations;

    public float timeBetweenExplanations;

    public GameObject explanationBox;
    public TMPro.TMP_Text explanationText;

    private Explanation current;
    private Queue<string> explanationsQueue;

    private TutorialManager tutorialManager;

    private void Start()
    {
        explanationsQueue = new Queue<string>();
        tutorialManager = GetComponent<TutorialManager>();
    }

    public void StartExplanation(int index)
    {
        current = explanations[index];

        explanationsQueue.Clear();

        foreach (string part in current.parts)
        {
            explanationsQueue.Enqueue(part);
        }

        explanationBox.SetActive(true);

        DisplayNextPart();
    }

    public void DisplayNextPart()
    {
        if (explanationsQueue.Count == 0)
        {
            EndExplanation();
            return;
        }

        string nextPart = explanationsQueue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(nextPart));
    }

    public void EndExplanation()
    {
        explanationBox.SetActive(false);

        current = null;

        // Update tutorial manager
        tutorialManager.UpdateProgress();
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

    public IEnumerator StartNewExplanation(int index, float time)
    {
        yield return new WaitForSeconds(time);
        StartExplanation(index);
    }
}
