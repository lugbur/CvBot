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
    
    public partial class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        #region Constructor
    
        /// <summary>
        /// Default constructor for QuestionRepository
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork dependency</param>        
    	public QuestionRepository(ModelUnitOfWork unitOfWork) : base(unitOfWork) { }
    
        #endregion
    }
}
