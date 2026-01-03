using Sukun.Application.Dtos.RemembranceContent.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Interfaces
{
    public interface ISourceResolverService
    {
        Task<Dictionary<Guid, SourcePreviewDto>> ResolveAsync(
            IEnumerable<RemembranceContent> contents);
    }
}