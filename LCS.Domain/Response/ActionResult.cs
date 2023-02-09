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
            ErrorMessagesList=new List<string>();
            IsSuccess=true;
        }
        public void AddError(string error)
        {
            ErrorMessagesList.Add(error);
            IsSuccess=false;
        }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessagesList { get; set; }

        public string Error()
        {
            StringBuilder er= new();
            foreach (var error in ErrorMessagesList)
            {
                er.AppendLine(error);
            }
            return er.ToString();
        }
    }
    public class ActionResult<T>: ActionResult where T : class
    {
        public T? Item { get; set; }
        
    }
}
