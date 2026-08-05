using UnityEngine;
using UnityEngine.Events;

public class QuestionOption : MonoBehaviour
{
    public UnityEvent onOptionPressed;

    public void ButtonClick()
    {
        onOptionPressed?.Invoke();
    }
}
