using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Response
{
    public class ActionResult
    {
        public ActionResult()
        {
            ErrorMessages=new List<string>();
            IsSuccess=true;
        }
        public void AddError(string error)
        {
            ErrorMessages.Add(error);
            IsSuccess=false;
        }
        public string FistError=> ErrorMessages.FirstOrDefault() ?? "No error message.";

        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }

    }
    public class ActionResult<T>: ActionResult where T : class
    {
        public ActionResult()
        {
            Items= new List<T>();
        }
        public T? Item { get; set; }
        public List<T> Items { get; set; }
        
    }
}
