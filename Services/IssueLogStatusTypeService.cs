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
	public class IssueLogStatusTypeService : BaseService, IIssueLogStatusTypeService
	{
		//GET CALL 
		public List<IssueLogStatusType> GetAll()
		{
			List<IssueLogStatusType> list = new List<IssueLogStatusType>(); //creates empty list

			DataProvider.ExecuteCmd("dbo.Logs_IssueLogStatusType_SelectAll",
				inputParamMapper: null, 
				singleRecordMapper: (IDataReader reader, short set) =>
				{
					list.Add(DataMapper<IssueLogStatusType>.Instance.MapToObject(reader));
				});

			return list;
		}

		//GET BY ID CALL 
		public IssueLogStatusType GetById(int id) 
		{
			IssueLogStatusType type = new IssueLogStatusType();

			DataProvider.ExecuteCmd("dbo.Logs_IssueLogStatusType_SelectById",
				inputParamMapper: (SqlParameterCollection paramCollection) =>  // or inputParamMapper: delegate (SqlParameterCollection paramCollection)  //"delegate" and "=>" are lambda expressions, they both "return"
				{
					paramCollection.AddWithValue("@Id", id);					
				},
				singleRecordMapper: (IDataReader reader, short set) => // or singleRecordMapper: delegate (IDataReader reader, short set)
				{
					type = DataMapper<IssueLogStatusType>.Instance.MapToObject(reader);
				});

			return type;			
		}

		//POST CALL 
		public int Post(IssueLogStatusTypeAddRequest model)
		{
			int id = 0;

			DataProvider.ExecuteNonQuery("dbo.Logs_IssueLogStatusType_Insert",
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
		public void Put(IssueLogStatusTypeUpdateRequest model)
		{
			DataProvider.ExecuteNonQuery("dbo.Logs_IssueLogStatusType_Update",
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
			DataProvider.ExecuteNonQuery("dbo.Logs_IssueLogStatusType_Delete",
				inputParamMapper: (SqlParameterCollection paramCollection) =>
				{
					paramCollection.AddWithValue("@Id", id);
				});
		}
	}
}
