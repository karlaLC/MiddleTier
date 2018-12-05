using Prospect.Models.Domain.Logs;
using Prospect.Models.Requests.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Services.Interfaces.Logs
{
    public interface IIssueLogStatusTypeService
    {
        List<IssueLogStatusType> GetAll();
        IssueLogStatusType GetById(int id);
        int Post(IssueLogStatusTypeAddRequest model);
        void Put(IssueLogStatusTypeUpdateRequest model);
        void Delete(int id);
    }
}