using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using Application.Results;

namespace Application
{
    /// <summary>
    /// Class ResultWrapper
    /// </summary>
    public class ResultWrapper
	{
		/// <summary>
		/// int Status
		/// </summary>
		public int Status { get; protected set; }

		/// <summary>
		/// Result Result
		/// </summary>
		public Result Result { get; set; }

		/// <summary>
		/// Constructor CreateResponse
		/// </summary>
		/// <param name="status">int</param>
		public static ResultWrapper CreateResponse(int status)
		{
			return new ResultWrapper { Status = status };
		}

		/// <summary>
		/// ResultWrapper Conflict
		/// </summary>
		public static ResultWrapper Conflict()
		{
			return CreateResponse(StatusCodes.Status409Conflict);
		}

		/// <summary>
		/// ResultWrapper NotFound
		/// </summary>
		public static ResultWrapper NotFound()
		{
			return CreateResponse(StatusCodes.Status404NotFound);
		}

		/// <summary>
		/// ResultWrapper BadGateway
		/// </summary>
		public static ResultWrapper BadGateway()
		{
			return CreateResponse(StatusCodes.Status502BadGateway);
		}

		/// <summary>
		/// ResultWrapper Ok
		/// </summary>
		/// <param name="obj">Type</param>
		public static ResultWrapper Ok<T>(T obj)
		{
			var response = CreateResponse(StatusCodes.Status200OK);
			response.Result = new Result<T>(obj);
			response.Result.Status = StatusCodes.Status200OK;
			return response;
		}

		/// <summary>
		/// ResultWrapper Error
		/// </summary>
		/// <param name="property">string</param>
		/// <param name="message">string</param>
		public static ResultWrapper Error(string property, string message)
		{
			var response = CreateResponse(StatusCodes.Status400BadRequest);
			response.Result = new Result();
			response.Result.Status = StatusCodes.Status400BadRequest;
			response.Result.AddNotification(property, message);
			return response;
		}

		/// <summary>
		/// ResultWrapper Error
		/// </summary>
		/// <param name="notifications">string</param>
		public static ResultWrapper Error(IReadOnlyCollection<Notification> notifications)
		{
			var response = CreateResponse(StatusCodes.Status400BadRequest);
			response.Result = new Result();
			response.Result.Status = StatusCodes.Status400BadRequest;
			response.Result.AddNotifications(notifications);
			return response;
		}

		/// <summary>
		/// ResultWrapper Created
		/// </summary>
		/// <param name="obj">Type</param>
		public static ResultWrapper Created<T>(T obj)
		{
			var response = CreateResponse(StatusCodes.Status201Created);
			response.Result = new Result<T>(obj);
			response.Result.Status = StatusCodes.Status201Created;
			return response;
		}

		/// <summary>
		/// ResultWrapper Accepted
		/// </summary>
		/// <param name="obj">Type</param>
		public static ResultWrapper Accepted<T>(T obj)
		{
			var response = CreateResponse(StatusCodes.Status202Accepted);
			response.Result = new Result<T>(obj);
			response.Result.Status = StatusCodes.Status202Accepted;
			return response;
		}
	}

	/// <summary>
	/// class Result
	/// </summary>
	public class Result
	{
		/// <summary>
		/// NotificationResult NotificationResult 
		/// </summary>
		public NotificationResult NotificationResult { get; } = new NotificationResult();

		/// <summary>
		/// GetErrors
		/// </summary>
		public IDictionary<string, string[]> Errors => GetErrors();

		private IDictionary<string, string[]> GetErrors()
		{
			var result = NotificationResult.Notifications.Select(n => KeyValuePair.Create(n.Property, new[] { n.Message })).ToArray();
			return result.ToDictionary(d => d.Key, v => v.Value);
		}

		/// <summary>
		/// Get NotificationResult
		/// </summary>
		/// <returns>NotificationResult</returns>
		public NotificationResult GetNotificationResult()
		{
			return NotificationResult;
		}

		/// <summary>
		/// AddNotifications
		/// </summary>
		/// <param name="notifications">notifications</param>
		public void AddNotifications(IReadOnlyCollection<Notification> notifications)
		{
			NotificationResult.AddNotifications(notifications);
		}

		/// <summary>
		/// AddNotification
		/// </summary>
		/// <param name="property">property</param>
		/// <param name="message">message</param>
		public void AddNotification(string property, string message)
		{
			NotificationResult.AddNotification(property, message);
		}

		/// <summary>
		/// int Status
		/// </summary>
		public int Status { get; set; }
	}

	/// <summary>
	/// class Result Type T
	/// </summary>
	public class Result<T> : Result
	{
		/// <summary>
		/// T Data
		/// </summary>
		public T Data { get; }

		/// <summary>
		/// Method Result
		/// </summary>
		/// <param name="data">Result type data</param>
		public Result(T data) => Data = data;
	}
}