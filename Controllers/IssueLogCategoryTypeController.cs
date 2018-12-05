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
	[RoutePrefix("api/issuelogcategorytypes")]
	public class IssueLogCategoryTypeController : ApiController
    {
		IIssueLogCategoryTypeService _issueLogCategoryTypeService;
        IErrorLogService _errorLogService;

		public IssueLogCategoryTypeController(IIssueLogCategoryTypeService issueLogCategoryTypeService, IErrorLogService errorLogService)
		{
			_issueLogCategoryTypeService = issueLogCategoryTypeService;
            _errorLogService = errorLogService;
		}

		//GET ALL
		[Route(), HttpGet]
		public IHttpActionResult GetAll()
		{
			try
			{
				ItemsResponse<IssueLogCategoryType> response = new ItemsResponse<IssueLogCategoryType>
				{
					Items = _issueLogCategoryTypeService.GetAll() //puts result of this function into items
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
				ItemResponse<IssueLogCategoryType> response = new ItemResponse<IssueLogCategoryType>
				{
					Item = _issueLogCategoryTypeService.GetById(id)
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

		//POST - ADD NEW ISSUE LOG CATEGORY TYPE
		[Route(), HttpPost]
		public IHttpActionResult Post(IssueLogCategoryTypeAddRequest model)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest(ModelState); }
				ItemResponse<int> response = new ItemResponse<int>
				{
					Item = _issueLogCategoryTypeService.Post(model)
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

		//PUT - EDIT OR UPDATE ISSUE LOG CATEGORY TYPE
		[Route(), HttpPut]
		public IHttpActionResult Put(IssueLogCategoryTypeUpdateRequest model)  
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest(ModelState); }
				_issueLogCategoryTypeService.Put(model);

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
				_issueLogCategoryTypeService.Delete(id);

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
