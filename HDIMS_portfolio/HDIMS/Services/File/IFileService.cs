using System.Collections.Generic;
using HDIMS.Models.Domain.Board;
namespace HDIMS.Services.File
{
    public interface IFileService
    {
        IList<IDictionary<string, string>> GetFileList(string BoardCd, string ContentCd);
        string IsFileExist(string BoardCd, string ContentCd);
        void DeleteFiles(IEnumerable<BoardModel> boards);
        void DeleteFiles(BoardModel board);
        void DeleteFiles(BoardContentModel boardContent);
        void DeleteFiles(string BoardCd, string ContentCd);
        void DeleteFile(string BoardCd, string ContentCd, string Guid);

    }
}