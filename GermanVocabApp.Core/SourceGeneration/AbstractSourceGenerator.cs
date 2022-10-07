using System.Text;

namespace GermanVocabApp.Core.SourceGeneration;

public abstract class AbstractSourceGenerator
{
    protected StringBuilder Sb { get; }

    protected AbstractSourceGenerator(StringBuilder sb)
    {
        Sb = sb;
    }
}
