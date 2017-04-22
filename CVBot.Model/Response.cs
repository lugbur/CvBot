//------------------------------------------------------------------------------
// LUGBUR
//------------------------------------------------------------------------------

using CVBot.BussinessLayer;

namespace CVBot.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Response : Entity
    {
        public int ResponseID { get; set; }
        public int QuestionID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    
        public virtual Question Question { get; set; }
    }
}
