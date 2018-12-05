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
	public class IssueLogCategoryTypeService : BaseService, IIssueLogCategoryTypeService
	{
		//GET CALL 
		public List<IssueLogCategoryType> GetAll()
		{
			List<IssueLogCategoryType> list = new List<IssueLogCategoryType>();

			DataProvider.ExecuteCmd("dbo.Logs_IssueLogCategoryType_SelectAll",
				inputParamMapper: null,
				singleRecordMapper: (IDataReader reader, short set) =>
				{
					list.Add(DataMapper<IssueLogCategoryType>.Instance.MapToObject(reader));
				});

			return list;
		}

		//GET BY ID CALL 
		public IssueLogCategoryType GetById(int id)
		{
			IssueLogCategoryType type = new IssueLogCategoryType();

			DataProvider.ExecuteCmd("dbo.Logs_IssueLogCategoryType_SelectById",
				inputParamMapper: (SqlParameterCollection paramCollection) =>
				{
					paramCollection.AddWithValue("@Id", id);
				},
				singleRecordMapper: (IDataReader reader, short set) =>
				{
					type = DataMapper<IssueLogCategoryType>.Instance.MapToObject(reader);
				});

			return type;
		}

		//POST CALL
		public int Post(IssueLogCategoryTypeAddRequest model)
		{
			int id = 0;

			DataProvider.ExecuteNonQuery("dbo.Logs_IssueLogCategoryType_Insert",
				inputParamMapper: (SqlParameterCollection paramCollection) =>
				{
					paramCollection.AddWithValue("@TypeName", model.TypeName);
					paramCollection.AddWithValue("@TypeDescription", model.TypeDescription);

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
		public void Put(IssueLogCategoryTypeUpdateRequest model)
		{
			DataProvider.ExecuteNonQuery("dbo.Logs_IssueLogCategoryType_Update",
				inputParamMapper: (SqlParameterCollection paramCollection) =>
				{
					paramCollection.AddWithValue("@TypeName", model.TypeName);
					paramCollection.AddWithValue("@TypeDescription", model.TypeDescription);
					paramCollection.AddWithValue("@Id", model.Id);
				});
		}

		//DELETE CALL 
		public void Delete(int id)
		{
			DataProvider.ExecuteNonQuery("dbo.Logs_IssueLogCategoryType_Delete",
				inputParamMapper: (SqlParameterCollection paramCollection) =>
				{
					paramCollection.AddWithValue("@Id", id);
				});
		}
	}
}
