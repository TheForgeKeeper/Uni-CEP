using System;

public interface IQuestion
{
    event Action<IQuestion, bool> OnAnswered;

}