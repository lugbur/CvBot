//===================================================================================
// © LUGBUR
//===================================================================================

#region

using CVBot.Model;
using CVBot.DataAccess.Repository;

#endregion

namespace CVBot.DataAccess.IRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial interface IResponseRepository : IGenericRepository<Response> { }
}
