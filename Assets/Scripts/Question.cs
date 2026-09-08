//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class Question : MonoBehaviour, IQuestion
//{
//    [SerializeField] private List<GameObject> options = new List<GameObject>(4);

//    [SerializeField] private UnityEvent onAnsweredCorrectly;
//    [SerializeField] private UnityEvent onAnsweredIncorrectly;

//    public event Action<IQuestion, bool> OnAnswered;



    

//    private void Awake()
//    {
//        foreach (GameObject option in options)
//        {
//            IQuestionOption questionOption;
//            if (option.TryGetComponent(out questionOption))
//            {
//                questionOption.onOptionPressed += handlePress;
//            }
//            else
//            {
//                Debug.LogError($"Option {option.name} does not implement IQuestionOption interface.");
//            }

//        }
//    }

//    private void handlePress(IQuestionOption option)
//    {
//        if (option.IsCorrectOption)
//        {
//            onAnsweredCorrectly?.Invoke();
//        }
//        else
//        {
//            onAnsweredIncorrectly?.Invoke();
//        }
//    }

//}
