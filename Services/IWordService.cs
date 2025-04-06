using BadgeMaker.Models;

namespace BadgeMaker.Services;

public interface IWordService : IDisposable
{
    bool PrintBadge(Badge badge);
}
