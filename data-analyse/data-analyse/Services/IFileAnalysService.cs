namespace data_analyse.Services
{
    public interface IFileAnalysService
    {

        Task AnalyseTextFileAsync(Guid uploadFileId, CancellationToken cancellationToken);
        Task AnalyseExelFileAsync(Guid uploadFileId, CancellationToken cancellationToken);

    }
}
