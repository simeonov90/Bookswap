namespace Bookswap.Application.Services.Covers.Dto
{
    public class CoverDto
    {
        public Guid? Id { get; set; }
        public byte[]? Bytes { get; set; }
        public string? Description { get; set; }
        public string? FileExtension { get; set; }
        public decimal? Size { get; set; }
        public string? ReadableFileSize { get; set; }
        public string? UnSupportedFileType { get; set;}
    }
}
