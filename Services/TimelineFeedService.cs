using Prospect.Models.Domain.Timelines;
using Prospect.Models.Requests.Timelines;
using Prospect.Models.Responses;
using Prospect.Services.Interfaces.Timelines;
using Prospect.Services.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Services.Timelines
{ 
    public class TimelineFeedService : BaseService, ITimelineFeedService
    {
        public List<TimelineFeed> GetAll()
        {
            List<TimelineFeed> list = new List<TimelineFeed>();

            DataProvider.ExecuteCmd("dbo.Timelines_TimelineFeed_SelectAll",
                inputParamMapper: null,
                singleRecordMapper: (IDataReader reader, short set) =>
                {
                    list.Add(DataMapper<TimelineFeed>.Instance.MapToObject(reader));
                });
            return list;
        }

		//GetAll() pulling details from joins with Events, Blogs, Workout and Nutrition Plans 
		public List<TimelineFeedResponse> GetAllJoinList(int currentUserBaseId)
		{
			List<TimelineFeedResponse> list = new List<TimelineFeedResponse>();

			DataProvider.ExecuteCmd("dbo.Timelines_TimelineFeedUserJoin_SelectByUserBaseId",
				inputParamMapper: delegate (SqlParameterCollection paramCollection)
				{
					paramCollection.AddWithValue("@currentUserBaseId", currentUserBaseId);
				},
				singleRecordMapper: (IDataReader reader, short set) =>
				{
					list.Add(DataMapper<TimelineFeedResponse>.Instance.MapToObject(reader));
				});
			return list;
		}				

		public TimelineFeed GetById(int id)
        {
            TimelineFeed timelineFeed = new TimelineFeed();
            DataProvider.ExecuteCmd("dbo.Timelines_TimelineFeed_SelectById",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                },
                singleRecordMapper: (IDataReader reader, short set) => 
                {
                    timelineFeed = (DataMapper<TimelineFeed>.Instance.MapToObject(reader));
                });
            return timelineFeed;
        }
			
		public int Post(TimelineFeedAddRequest model)
        {
            int id = 0;
            DataProvider.ExecuteNonQuery("dbo.Timelines_TimelineFeed_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@TimelineFeedTypeId", model.TimelineFeedTypeId);
                    paramCollection.AddWithValue("@FeedContent", model.FeedContent);
                    paramCollection.AddWithValue("@IsPublic", model.IsPublic);
                    paramCollection.AddWithValue("@IsSubscriptionOnly", model.IsSubscriptionOnly);
                    paramCollection.AddWithValue("@IsArchived", model.IsArchived);
                    paramCollection.AddWithValue("@CreatedById", model.CreatedById);
                    //paramCollection.AddWithValue("@ModifiedById", model.CreatedById);
					SqlParameter paramId = new SqlParameter("@Id", SqlDbType.Int);
                    paramId.Direction = ParameterDirection.Output;
                    paramId.Value = id;
                    paramCollection.Add(paramId);
                },
                returnParameters: (SqlParameterCollection paramCollection) =>
                {
                    int.TryParse(paramCollection["@Id"].Value.ToString(), out id);
                });
            return id; 
        }

        public int Put(TimelineFeedUpdateRequest model)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery("dbo.Timelines_TimelineFeed_Update",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@TimelineFeedTypeId", model.TimelineFeedTypeId);
                    paramCollection.AddWithValue("@FeedContent", model.FeedContent);
                    paramCollection.AddWithValue("@IsPublic", model.IsPublic);
                    paramCollection.AddWithValue("@IsSubscriptionOnly", model.IsSubscriptionOnly);
                    paramCollection.AddWithValue("@IsArchived", model.IsArchived);
                    paramCollection.AddWithValue("@CreatedById", model.CreatedById);
                    paramCollection.AddWithValue("@ModifiedById", model.ModifiedById);
                    paramCollection.AddWithValue("@Id", model.Id);
                });
            return id;
        }

        public int Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.Timelines_TimelineFeed_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@id", id);
                });
            return id;
        }
    }
}
