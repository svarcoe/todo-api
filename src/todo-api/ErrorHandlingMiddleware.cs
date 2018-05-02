﻿using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Todo.Api
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			this._next = next;
		}

		public async Task Invoke(HttpContext context /* other dependencies */)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected
			string result = JsonConvert.SerializeObject(new { type = exception.GetType().FullName, error = exception.Message, stacktrace = exception.Demystify().StackTrace });
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;
			return context.Response.WriteAsync(result);
		}
	}
}
