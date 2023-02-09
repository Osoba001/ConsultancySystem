using LCS.Domain.Models;
using LCS.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMediatR.MediatRContract
{
    public interface ICommand
    {
        ActionResult Validate();
    }

}   
