using Bookswap.Application.Services.Covers.Dto;
using Bookswap.Application.Services.Shared;

namespace Bookswap.Application.Services.Covers
{
    public interface ICoverService : IBaseService<CoverDto, CreateCoverDto, UpdateCoverDto, Guid>
    {
    }
}
