using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Data.DTO;
using Todo.Data.DTO.Board;
using Todo.Data.DTO.BoardAssignment;

namespace Todo.Service.Board
{
    public interface IBoardService
    {
        ApiResponseDTO GetAll();
        ApiResponseDTO Add(CreateBoardDTO model);
        ApiResponseDTO Update(UpdateBoardDTO model);
        ApiResponseDTO Delete(DeleteBoardDTO model);
    }
}
