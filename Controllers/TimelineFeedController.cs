using Prospect.Models.Domain.Timelines;
using Prospect.Models.Requests.Timelines;
using Prospect.Models.Responses;
using Prospect.Services;
using Prospect.Services.Interfaces.Logs;
using Prospect.Services.Interfaces.Timelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Prospect.Models.Requests.Logs; 

namespace Prospect.Web.Controllers.Api.Timelines
{
	//[AllowAnonymous] //uncomment to test on Rest Client
	[RoutePrefix("api/timelinefeeds")]
    public class TimelineFeedController : ApiController
    {
        ITimelineFeedService _timelineFeedService;
        IErrorLogService _errorLogService;
		IAuthenticationService _authenticationService;

		
        public TimelineFeedController(ITimelineFeedService timelineFeedService, IErrorLogService errorLogService, IAuthenticationService authenticationService)
        {
			_timelineFeedService = timelineFeedService;
            _errorLogService = errorLogService;
			_authenticationService = authenticationService;
        }	

		//GET ALL FOR JOIN WITH USER PROFILE 
		[Route(), HttpGet]
		public IHttpActionResult GetAllJoinList()
		{
			try
			{
				ItemsResponse<TimelineFeedResponse> response = new ItemsResponse<TimelineFeedResponse>
				{
					Items = _timelineFeedService.GetAllJoinList(_authenticationService.GetCurrentUserId())
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
				return (BadRequest(ex.Message));
			}
		}

		//POST - TO CREATE A NEW POST OR STATUS UPDATE IN THE TIMELINE FEED
		[Route(), HttpPost]
        public IHttpActionResult Post(TimelineFeedAddRequest model)
        {			
            try
            {
				model.CreatedById = _authenticationService.GetCurrentUserId();  //this grabs the logged in user's id (for new posts)
				if (!ModelState.IsValid) { return BadRequest(ModelState); }
                ItemResponse<int> response = new ItemResponse<int>
                {
                    Item = _timelineFeedService.Post(model)
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

		//DELETE (SINGLE POST)
        [Route("{id:int}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {                
				_timelineFeedService.Delete(id);
                
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
            };
        }
    }

}
    
