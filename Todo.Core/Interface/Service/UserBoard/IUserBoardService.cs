using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public interface IUserBoardService
    {
        ApiResponseDTO BoardsByUserId(GetUsersBoardDTO model);
        ApiResponseDTO AddUserBoard(UsersBoardDTO model);
        ApiResponseDTO RemoveUserBoard(UsersBoardDTO model);
        ApiResponseDTO UsersByBoardId(UsersBoardDTO model);
    }
}
