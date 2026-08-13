using System;
using UnityEngine;
using UnityEngine.Events;

public class QuestionOption : MonoBehaviour, IQuestionOption

{
    [SerializeField] private bool isCorrectOption;

    public bool IsCorrectOption => isCorrectOption;
    public event Action<IQuestionOption> onOptionPressed;


    public void ButtonClick()
    {
        onOptionPressed?.Invoke(this);
    }
}
