using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.Board;

namespace HDIMS.Services.Board
{
    public interface IBoardService
    {
        IList<BoardModel> SelectBoard(Hashtable param);
        int InsertBoard(IEnumerable<BoardModel> param);
        int UpdateBoard(IEnumerable<BoardModel> param);
        int DeleteBoard(IEnumerable<BoardModel> param);

        IList<BoardContentModel> SelectContent(Hashtable param);
        int SelectContentCnt(Hashtable param);
        string InsertContent(BoardContentModel param);
        int UpdateContent(BoardContentModel param);
        int DeleteContent(BoardContentModel param);
        int CountReadContent(BoardContentModel param);

        IList<BoardReplyModel> SelectReply(Hashtable param);
        int InsertContent(BoardReplyModel param);
        int DeleteContent(BoardReplyModel param);

    }
}