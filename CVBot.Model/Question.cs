//------------------------------------------------------------------------------
// LUGBUR
//------------------------------------------------------------------------------

using CVBot.BussinessLayer;

namespace CVBot.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Question : Entity
    {
        public Question()
        {
            this.Response = new HashSet<Response>();
        }
    
        public int QuestionID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ICollection<Response> Response { get; set; }
    }
}
