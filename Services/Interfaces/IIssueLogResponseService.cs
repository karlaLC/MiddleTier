using Prospect.Models.Domain.Logs;
using Prospect.Models.Requests.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Services.Interfaces.Logs
{
    public interface IIssueLogResponseService
    {
        List<IssueLogResponse> GetAll();
        List<DetailedIssueLogReponse> GetDetailedAll();
        DetailedIssueLogReponse GetById(int id);
        int Post(IssueLogResponseAddRequest model);
        void Put(IssueLogResponseUpdateRequest model);
        void Delete(int id);
    }
}