using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.ActionResponse
{
    public class ActionResult
    {
        public ActionResult()
        {
            ErrorMessagesList = new List<string>();
            IsSuccess = true;
        }
        public void AddError(string error)
        {
            ErrorMessagesList.Add(error);
            IsSuccess = false;
        }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessagesList { get; set; }

        public string Errors()
        {
            StringBuilder er = new();
            foreach (var error in ErrorMessagesList)
            {
                er.AppendLine(error);
            }
            return er.ToString();
        }
        public override string ToString()
        {
            return Errors();
        }
        public object data { get; set; }
    }
    public class ActionResult<T> : ActionResult where T : class
    {
        public T? Item { get; set; }

    }
}
