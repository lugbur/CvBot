//------------------------------------------------------------------------------
// LUGBUR
//------------------------------------------------------------------------------

using CVBot.BussinessLayer;

namespace CVBot.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Category : Entity
    {
        public Category()
        {
            this.Question = new HashSet<Question>();
        }
    
        public int CategoryID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Question> Question { get; set; }
    }
}
