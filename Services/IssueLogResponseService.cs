using Prospect.Models.Domain.Logs;
using Prospect.Models.Requests.Logs;
using Prospect.Services.Interfaces.Logs;
using Prospect.Services.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Services.Logs
{
	public class IssueLogResponseService : BaseService, IIssueLogResponseService
	{
		//GET CALL 
		public List<IssueLogResponse> GetAll()
		{
			List<IssueLogResponse> list = new List<IssueLogResponse>();

			DataProvider.ExecuteCmd("dbo.Logs_IssueLogResponse_SelectAll",
				inputParamMapper: null,
				singleRecordMapper: (IDataReader reader, short set) =>
				{
					list.Add(DataMapper<IssueLogResponse>.Instance.MapToObject(reader));
				});

			return list;
		}

		//GET CALL FOR ISSUE LOG RESPONSE WITH EXTRA DETAILS (ITERATION, ADDS CreatorFirstName, CreatorLastName, RoleName)
        public List<DetailedIssueLogReponse> GetDetailedAll()
        {
            List<DetailedIssueLogReponse> list = new List<DetailedIssueLogReponse>();

            DataProvider.ExecuteCmd("dbo.Logs_IssueLogResponse_DisplayComm",
                inputParamMapper: null,
                singleRecordMapper: (IDataReader reader, short set) =>
                {
                    list.Add(DataMapper<DetailedIssueLogReponse>.Instance.MapToObject(reader));
                });

            return list;
        }

		//GET BY ID FOR ISSUE LOG RESPONSE WITH EXTRA DETAILS 
        public DetailedIssueLogReponse GetById(int id)
		{
			DetailedIssueLogReponse issueLog = new DetailedIssueLogReponse();

			DataProvider.ExecuteCmd("dbo.Logs_IssueLogResponse_SelectById",
				inputParamMapper: (SqlParameterCollection paramCollection) =>
				{
					paramCollection.AddWithValue("@Id", id);
				},
				singleRecordMapper: (IDataReader reader, short set) =>
				{
					issueLog = DataMapper<DetailedIssueLogReponse>.Instance.MapToObject(reader); 
				});

			return issueLog;
		}

		//POST CALL
		public int Post(IssueLogResponseAddRequest model)
		{
			int id = 0;

			DataProvider.ExecuteNonQuery("dbo.Logs_IssueLogResponse_Insert",
				inputParamMapper: (SqlParameterCollection paramCollection) =>
				{
					paramCollection.AddWithValue("@IssueLogId", model.IssueLogId);
					paramCollection.AddWithValue("@ResponseText", model.ResponseText);
					paramCollection.AddWithValue("@CreatedById", model.CreatedById);
					//DO NOT INCLUDE @CreatedDate

					//TO GET NEW ID OUT/BACK 
					SqlParameter paramId = new SqlParameter("@Id", SqlDbType.Int);
					paramId.Direction = ParameterDirection.Output;
					paramId.Value = id;
					paramCollection.Add(paramId);
				},
				returnParameters: (SqlParameterCollection parameterCollection) =>
				{
					int.TryParse(parameterCollection["@Id"].Value.ToString(), out id);
				});

			return id;
		}

		//PUT CALL
		public void Put(IssueLogResponseUpdateRequest model)
		{
			DataProvider.ExecuteNonQuery("dbo.Logs_IssueLogResponse_Update",
				inputParamMapper: (SqlParameterCollection paramCollection) =>
				{
					paramCollection.AddWithValue("@IssueLogId", model.IssueLogId);
					paramCollection.AddWithValue("@ResponseText", model.ResponseText);			
					paramCollection.AddWithValue("@Id", model.Id);
				});
		}
		
		//DELETE CALL 
		public void Delete(int id)
		{
			DataProvider.ExecuteNonQuery("dbo.Logs_IssueLogResponse_Delete",
				inputParamMapper: (SqlParameterCollection paramCollection) =>
				{
					paramCollection.AddWithValue("@Id", id);
				});
		}
	}
}

