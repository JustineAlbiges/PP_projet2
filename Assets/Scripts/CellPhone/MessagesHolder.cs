using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] _answerBoxes;
    [SerializeField] private GameObject[] _questionBoxes;
    public bool clickable = true;
    private int _index = 0;
    private float _scrollSpeed = 5f;
    private int _slideDistance = 40;

    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void StartAnswerSlide()
    {
        clickable = false;
        StartCoroutine(Slide(true));
    }
    public void StartQuestionSlide()
    {
        StartCoroutine(Slide(false));
    }
    private IEnumerator Slide(bool answer)
    {
        var initPos = rect.position.y;
        Debug.Log("Init pos: " + initPos);
        var endPos = rect.position.y + _slideDistance;
        Debug.Log("Init pos: " + endPos);

        while(rect.position.y < endPos)
        {
            rect.Translate(Vector3.up * _scrollSpeed);
            yield return new WaitForFixedUpdate();
        }

        if(answer) ShowAnswer();
        else ShowNextQuestion();
    }

    private void ShowAnswer()
    {
        _answerBoxes[_index].SetActive(true);
        Invoke("StartQuestionSlide", 3f);
    }

    private void ShowNextQuestion()
    {
        _questionBoxes[_index].SetActive(true);
        _index++;
        clickable = true;
    }
}
