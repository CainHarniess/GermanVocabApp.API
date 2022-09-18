namespace GermanVocabApp.Core.Exceptions;

public class UnexpectedIdException : Exception
{
    public UnexpectedIdException()
    {

    }

    public UnexpectedIdException(string message) : base(message)
    {

    }

    public UnexpectedIdException(string message, Exception inner)
        : base(message, inner)
    {

    }
}
