using Prospect.Models.Domain.Logs;
using Prospect.Models.Requests.Logs;
using Prospect.Models.Responses;
using Prospect.Services;
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
	[RoutePrefix("api/issuelogresponses")]
    public class IssueLogResponseController : ApiController
    {
        IIssueLogResponseService _issueLogResponseService;
        IAuthenticationService _authenticationService;
        IErrorLogService _errorLogService;

        public IssueLogResponseController(IIssueLogResponseService issueLogResponseService, IAuthenticationService authenticationService, IErrorLogService errorLogService)
        {
            _issueLogResponseService = issueLogResponseService;
            _authenticationService = authenticationService;        
            _errorLogService = errorLogService;
        }

		//GET ALL 
		[Route(), HttpGet]
		public IHttpActionResult GetAll()
		{
			try
			{
				ItemsResponse<IssueLogResponse> response = new ItemsResponse<IssueLogResponse>
				{
					Items = _issueLogResponseService.GetAll()
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

		//GET ALL WITH EXTRA DETAILS (INCLUDES CreatorFirstName, CreatorLastName, RoleName)
		[Route("{detailed}"), HttpGet]
		public IHttpActionResult GetDetailedAll()
		{
			try
			{
				ItemsResponse<DetailedIssueLogReponse> response = new ItemsResponse<DetailedIssueLogReponse>
				{
					Items = _issueLogResponseService.GetDetailedAll()
				};

				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET BY ID - GETS BY THE ISSUE LOG ID RATHER THAN THE ISSUE LOG RESPONSE ID
		[Route("{id:int}"), HttpGet]
		public IHttpActionResult GetById(int id)  //GET BY ID will get by the Issue log ID rather than the issue log response ID
		{
			try
			{
				ItemResponse<DetailedIssueLogReponse> response = new ItemResponse<DetailedIssueLogReponse>
				{
					Item = _issueLogResponseService.GetById(id)
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
        public IHttpActionResult Post(IssueLogResponseAddRequest model) 
        {
            try
            {
                model.CreatedById = _authenticationService.GetCurrentUserId();
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                ItemResponse<int> response = new ItemResponse<int>
                {
                    Item = _issueLogResponseService.Post(model)
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _errorLogService.Post(new Models.Requests.Logs.ErrorLogAddRequest
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
        public IHttpActionResult Put(IssueLogResponseUpdateRequest model)
        {
            try
            {
                model.CreatedById = _authenticationService.GetCurrentUserId();
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                _issueLogResponseService.Put(model);

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
                _issueLogResponseService.Delete(id);

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
