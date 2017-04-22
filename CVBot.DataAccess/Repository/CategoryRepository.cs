//===================================================================================
// © LUGBUR
//===================================================================================

#region

using CVBot.Model;
using CVBot.DataAccess.IRepository;

#endregion

namespace CVBot.DataAccess.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        #region Constructor
    
        /// <summary>
        /// Default constructor for CategoryRepository
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork dependency</param>        
    	public CategoryRepository(ModelUnitOfWork unitOfWork) : base(unitOfWork) { }
    
        #endregion
    }
}
