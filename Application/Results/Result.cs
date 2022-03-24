using Application.Results;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Application
{

    public class ResultWrapper
    {
        public int Status { get; protected set; }

        public Result Result { get; set; }

        public static ResultWrapper CreateResponse(int status)
        {
            return new ResultWrapper { Status = status };
        }

        public static ResultWrapper Conflict()
        {
            return CreateResponse(StatusCodes.Status409Conflict);
        }

        public static ResultWrapper NotFound()
        {
            return CreateResponse(StatusCodes.Status404NotFound);
        }

        public static ResultWrapper BadGateway()
        {
            return CreateResponse(StatusCodes.Status502BadGateway);
        }

        public static ResultWrapper Ok<T>(T obj)
        {
            var response = CreateResponse(StatusCodes.Status200OK);
            response.Result = new Result<T>(obj);
            response.Result.Status = StatusCodes.Status200OK;
            return response;
        }

        public static ResultWrapper Error(string property, string message)
        {
            var response = CreateResponse(StatusCodes.Status400BadRequest);
            response.Result = new Result();
            response.Result.Status = StatusCodes.Status400BadRequest;
            response.Result.AddNotification(property, message);
            return response;
        }

        public static ResultWrapper Error(IReadOnlyCollection<Notification> notifications)
        {
            var response = CreateResponse(StatusCodes.Status400BadRequest);
            response.Result = new Result();
            response.Result.Status = StatusCodes.Status400BadRequest;
            response.Result.AddNotifications(notifications);
            return response;
        }

        public static ResultWrapper Created<T>(T obj)
        {
            var response = CreateResponse(StatusCodes.Status201Created);
            response.Result = new Result<T>(obj);
            response.Result.Status = StatusCodes.Status201Created;
            return response;
        }

        public static ResultWrapper Accepted<T>(T obj)
        {
            var response = CreateResponse(StatusCodes.Status202Accepted);
            response.Result = new Result<T>(obj);
            response.Result.Status = StatusCodes.Status202Accepted;
            return response;
        }
    }

    public class Result
    {
        public NotificationResult NotificationResult { get; } = new NotificationResult();

        public IDictionary<string, string[]> Errors => GetErrors();

        private IDictionary<string, string[]> GetErrors()
        {
            var result = NotificationResult.Notifications.Select(n => KeyValuePair.Create(n.Property, new[] { n.Message })).ToArray();
            return result.ToDictionary(d => d.Key, v => v.Value);
        }

        public NotificationResult GetNotificationResult()
        {
            return NotificationResult;
        }

        public void AddNotifications(IReadOnlyCollection<Notification> notifications)
        {
            NotificationResult.AddNotifications(notifications);
        }

        public void AddNotification(string property, string message)
        {
            NotificationResult.AddNotification(property, message);
        }

        public int Status { get; set; }
    }

    public class Result<T> : Result
    {
        public T Data { get; }

        public Result(T data) => Data = data;
    }
}