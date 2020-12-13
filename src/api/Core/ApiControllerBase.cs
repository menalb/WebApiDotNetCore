using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProductsApi.Core
{
    public class ApiControllerBase<TController> : ControllerBase
    {
        protected readonly ILogger<TController> _logger;
        public ApiControllerBase(ILogger<TController> logger) =>
            _logger = logger;

        protected ActionResult ParsePostResponse<TItem>(InsertServiceResponse response) where TItem : BaseEntity =>
            response switch
            {
                InsertConflict => Conflict(),
                InsertOkResponse<TItem> content => Created("GetById", new { id = content.item.Id }),
                _ => throw new ArgumentException($"Unhandled case {nameof(response)}")
            };

        protected ActionResult ParsePutResponse<TItem>(EditServiceResponse response) where TItem : BaseEntity =>
            response switch
            {
                EditNotFoundResponse => NotFound(),
                EditOkResponse<TItem> content => Ok(content.item),
                _ => throw new ArgumentException($"Unhandled case {nameof(response)}")
            };

        protected ActionResult ParseDeleteResponse<TItem>(DeleteServiceResponse response) where TItem : BaseEntity =>
            response switch
            {
                DeleteOkResponse => Ok(),
                _ => throw new ArgumentException($"Unhandled case {nameof(response)}")
            };

        protected ActionResult ParseGetResponse<TItem>(TItem response) =>
            response switch
            {
                null => NotFound(),
                TItem content => Ok(content)
            };
    }
}