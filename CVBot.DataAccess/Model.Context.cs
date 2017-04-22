//===================================================================================
// © LUGBUR
//===================================================================================

#region

using CVBot.Model;

#endregion

namespace CVBot.DataAccess
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class CvBotDBEntities : DbContext
    {
        public CvBotDBEntities()
            : base("name=CvBotDBEntities")
        {
        }
    
    	public CvBotDBEntities(string nameOrConnectionString) : base(nameOrConnectionString)
    	{	
    	}
    
    	public CvBotDBEntities(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model)
    	{
    	}
    
    	public CvBotDBEntities(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
    	{
    	}
    
    	public CvBotDBEntities(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
    	{
    	}
    
    	protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Response> Response { get; set; }
    }
}
