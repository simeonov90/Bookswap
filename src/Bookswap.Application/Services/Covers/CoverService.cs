using AutoMapper;
using Bookswap.Application.Extensions.Methods;
using Bookswap.Application.Services.Covers.Dto;
using Bookswap.Domain.Models;
using Bookswap.Infrastructure.Extensions.Models;
using Bookswap.Infrastructure.UOW.IUOW;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Bookswap.Application.Services.Covers
{
    public class CoverService : ICoverService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<CoverService> logger;

        public CoverService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CoverService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<CoverDto> CreateAsync(CreateCoverDto createCoverDto)
        {
            var fileExtension = Path.GetExtension(createCoverDto.FormFile.FileName);
            if (fileExtension.IsValidImageType() is false)
            {
                return new CoverDto() { UnSupportedFileType = $"Unsupported file type: {fileExtension}" };
            }

            if (createCoverDto.FormFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await createCoverDto.FormFile.CopyToAsync(memoryStream);

                    if (memoryStream.Length < 2097152)
                    {
                        var entity = new Cover
                        {
                            Bytes = memoryStream.ToArray(),
                            Size = memoryStream.Length,
                            Description = createCoverDto.FormFile.FileName,
                            FileExtension = fileExtension
                        };

                        await unitOfWork.Cover.Add(entity);
                        await unitOfWork.CompletedAsync();

                        return new CoverDto
                        {
                            Id = entity.Id,
                            Size = entity.Size,
                            Bytes = entity.Bytes,
                            Description = entity.Description,
                            FileExtension = entity.FileExtension,
                            ReadableFileSize = HumanReadableFileSize.ReadableFileSize(memoryStream.Length)
                        };
                    }

                    throw new ArgumentOutOfRangeException($"Image size cannot be more than 2mb. Your image is {HumanReadableFileSize.ReadableFileSize(memoryStream.Length)}");
                }
            }

            return null;
        }

        public async Task DeleteAsync(Guid id)
        {
            await unitOfWork.Cover.Delete(id, false);
            await unitOfWork.CompletedAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await unitOfWork.Cover.Exists(a => a.Id == id);
        }

        public async Task<IEnumerable<CoverDto>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<CoverDto>>(await unitOfWork.Cover.GetAllAsync());
        }

        public async Task<PagingPagedResult<CoverDto>> GetAllAsync(PagingQueryParameters queryParameters)
        {
            return await unitOfWork.Cover.GetAllAsync<CoverDto>(queryParameters);
        }

        public async Task<CoverDto> GetById(Guid id)
        {
            return mapper.Map<CoverDto>(await unitOfWork.Cover.GetById(id));
        }

        public Task UpdateAsync(UpdateCoverDto updateAuthorDto)
        {
            throw new NotImplementedException();
        }
    }
}
