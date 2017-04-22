//===================================================================================
// © LUGBUR
//===================================================================================

#region

using CVBot.Model;

#endregion

namespace CVBot.DataAccess.Repository
{
    using IRepository;
    using System;
    using System.Collections.Generic;

    public partial class ResponseRepository : GenericRepository<Response>, IResponseRepository
    {
        #region Constructor
    
        /// <summary>
        /// Default constructor for ResponseRepository
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork dependency</param>        
    	public ResponseRepository(ModelUnitOfWork unitOfWork) : base(unitOfWork) { }
    
        #endregion
    }
}
