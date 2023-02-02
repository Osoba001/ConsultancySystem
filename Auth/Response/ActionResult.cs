using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Response
{
    public class ActionResult
    {
        public ActionResult()
        {
            ErrorMessages = new List<string>();
            IsSuccess = true;
        }
        public void AddError(string error)
        {
            ErrorMessages.Add(error);
            IsSuccess = false;
        }
        public string FistError => ErrorMessages.FirstOrDefault() ?? "No error message.";

        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
    public class ActionResult<T> where T : class
    {
        public ActionResult()
        {
            ErrorMessages = new List<string>();
            IsSuccess = true;
        }
        public void AddError(string error)
        {
            ErrorMessages.Add(error);
            IsSuccess = false;
        }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public T? Entity { get; set; }
        public string FistError => ErrorMessages.FirstOrDefault() ?? "No error message.";
    }
}
