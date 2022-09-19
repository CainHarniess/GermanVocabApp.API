namespace GermanVocabApp.Core.Exceptions;
public class UnexpectedNullIdException : Exception
{
    public UnexpectedNullIdException()
    {

    }

    public UnexpectedNullIdException(string message) : base(message)
    {

    }

    public UnexpectedNullIdException(string message, Exception inner)
        : base(message, inner)
    {

    }
}
