using System;
using UnityEngine.Events;

public interface IQuestionOption
{
    bool IsCorrectOption { get; }
    public event Action<IQuestionOption> onOptionPressed;

}