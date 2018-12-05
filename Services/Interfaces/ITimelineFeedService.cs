using Prospect.Models.Domain.Timelines;
using Prospect.Models.Requests.Timelines;
using Prospect.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Services.Interfaces.Timelines
{
    public interface ITimelineFeedService
    {
        List<TimelineFeed> GetAll();
		List<TimelineFeedResponse> GetAllJoinList(int currentUserBaseId); //Karla 
        TimelineFeed GetById(int id);
        int Post(TimelineFeedAddRequest model);
        int Put(TimelineFeedUpdateRequest model);
        int Delete(int id);
    }
}
