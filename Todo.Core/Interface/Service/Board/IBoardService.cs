using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Todo.Core
{
    public interface IBoardService
    {
        ApiResponseDTO GetAll();
        ApiResponseDTO GetActiveBoards();
        ApiResponseDTO Add(CreateBoardDTO model);
        ApiResponseDTO Update(UpdateBoardDTO model);
        ApiResponseDTO Delete(DeleteBoardDTO model);
    }
}
