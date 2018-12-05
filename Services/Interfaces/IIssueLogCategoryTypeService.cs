using Prospect.Models.Domain.Logs;
using Prospect.Models.Requests.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Services.Interfaces.Logs
{
    public interface IIssueLogCategoryTypeService
    {
        List<IssueLogCategoryType> GetAll();
        IssueLogCategoryType GetById(int id);
        int Post(IssueLogCategoryTypeAddRequest model);
        void Put(IssueLogCategoryTypeUpdateRequest model);
        void Delete(int id);
    }
}

