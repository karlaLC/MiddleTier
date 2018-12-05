using Prospect.Models.Domain.Logs;
using Prospect.Models.Requests.Logs;
using Prospect.Models.Responses;
using Prospect.Services.Interfaces.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Prospect.Web.Controllers.Api.Logs
{
	//[AllowAnonymous] //uncomment to test on Rest Client
	[RoutePrefix("api/issuelogstatustypes")]
    public class IssueLogStatusTypeController : ApiController
    {
		IIssueLogStatusTypeService _issueLogStatusTypeService;
        IErrorLogService _errorLogService;

		public IssueLogStatusTypeController(IIssueLogStatusTypeService issueLogStatusTypeService, IErrorLogService errorLogService)
		{
			_issueLogStatusTypeService = issueLogStatusTypeService;
            _errorLogService = errorLogService;
		}

		//GET ALL
		[Route(), HttpGet]
		public IHttpActionResult GetAll()
		{
			try
			{
				ItemsResponse<IssueLogStatusType> response = new ItemsResponse<IssueLogStatusType>
				{
					Items = _issueLogStatusTypeService.GetAll()
				};

				return Ok(response);
			}
			catch (Exception ex)
			{
				_errorLogService.Post(new ErrorLogAddRequest
				{
					ErrorSourceTypeId = 1,
					Message = ex.Message,
					StackTrace = ex.StackTrace,
					Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name
				});

				return BadRequest(ex.Message);
			}
		}

		//GET BY ID
		[Route("{id:int}"), HttpGet]
		public IHttpActionResult GetById(int id)
		{
			try
			{
				ItemResponse<IssueLogStatusType> response = new ItemResponse<IssueLogStatusType>
				{
					Item = _issueLogStatusTypeService.GetById(id)
				};

				return Ok(response);
			}
			catch (Exception ex)
			{
				_errorLogService.Post(new ErrorLogAddRequest
				{
					ErrorSourceTypeId = 1,
					Message = ex.Message,
					StackTrace = ex.StackTrace,
					Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name
				});

				return BadRequest(ex.Message);
			}
		}

		//POST 
		[Route(), HttpPost] 
		public IHttpActionResult Post(IssueLogStatusTypeAddRequest model)  
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest(ModelState); } 
				ItemResponse<int> response = new ItemResponse<int>
				{
					Item = _issueLogStatusTypeService.Post(model)
				};

				return Ok(response);
			}
			catch (Exception ex)
			{
                _errorLogService.Post(new ErrorLogAddRequest
                {
                    ErrorSourceTypeId = 1,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name
                });

                return BadRequest(ex.Message);
			}
		}

		//PUT
		[Route(), HttpPut]  
		public IHttpActionResult Put(IssueLogStatusTypeUpdateRequest model)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest(ModelState); }
				_issueLogStatusTypeService.Put(model);

				return Ok(new SuccessResponse());
			}
			catch (Exception ex)
			{
                _errorLogService.Post(new ErrorLogAddRequest
                {
                    ErrorSourceTypeId = 1,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name
                });

                return BadRequest(ex.Message);
			}
		}

		//DELETE
		[Route("{id:int}"), HttpDelete]  
		public IHttpActionResult Delete(int id)                       
		{
			try
			{				
				_issueLogStatusTypeService.Delete(id);

				return Ok(new SuccessResponse());
			}
			catch (Exception ex)
			{
                _errorLogService.Post(new ErrorLogAddRequest
                {
                    ErrorSourceTypeId = 1,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name
                });

                return BadRequest(ex.Message);
			}
		}		
	}
}
