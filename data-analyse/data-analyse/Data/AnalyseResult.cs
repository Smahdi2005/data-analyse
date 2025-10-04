namespace data_analyse.Data
{
    public class AnalyseResult
    {
        public Guid Id { get; set; }
        public Guid UploadFileId { get; set; }
        public string ResultJson { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
