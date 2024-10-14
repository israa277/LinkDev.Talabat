using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Controllers.Exceptions;
using System.Net;

namespace LinkDev.Talabat.APIs.Middlewares
{
	// Convension - Based
	public class CustomExceptionHandlerMiddelware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<CustomExceptionHandlerMiddelware> _logger;
		private readonly IWebHostEnvironment _env;

		public CustomExceptionHandlerMiddelware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddelware> logger, IWebHostEnvironment env)
		{
			_next = next;
			_logger = logger;
			_env = env;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				// Logic Executed with the Request
				await _next(httpContext);

				// Logic Executed with the Response

			}
			catch (Exception ex)
			{
				ApiResponse response;

				switch (ex)
				{
					case NotFoundException:
						httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

						httpContext.Response.ContentType = "application/json";

						 response = new ApiResponse(404, ex.Message);


						await httpContext.Response.WriteAsync(response.ToString());
						break;
					default:
						if (_env.IsDevelopment())
						{
							// Development Mode
							_logger.LogError(ex, ex.Message);
							response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString());
						}
						else
						{
							// Production Mode
							// Log Exception Details in DB

							response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
						}


						httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						httpContext.Response.ContentType = "application/json";
						await httpContext.Response.WriteAsync(response.ToString());

						break;
				}


			}
		}

	}
}
