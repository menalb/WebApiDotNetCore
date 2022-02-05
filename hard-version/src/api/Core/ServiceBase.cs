namespace ProductsApi.Core
{
    public interface ServiceResponse { }
    public record NotFoundServiceResponse : ServiceResponse { }

    public record FoundServiceResponse<TItem> : OkResponse<TItem>, ServiceResponse
    {
        public FoundServiceResponse(TItem original) : base(original) { }
    }

    public record OkResponse<TItem>
    {
        public OkResponse(TItem foundItem) { item = foundItem; }
        public TItem item { get; init; }
    }

    public interface InsertServiceResponse : ServiceResponse { }
    public record InsertConflict : InsertServiceResponse { }
    public record InsertOkResponse<TItem> : OkResponse<TItem>, InsertServiceResponse
    {
        public InsertOkResponse(TItem original) : base(original) { }
    }

    public interface EditServiceResponse : ServiceResponse { }
    public record EditNotFoundResponse : NotFoundServiceResponse, EditServiceResponse { }
    public record EditOkResponse<TItem> : OkResponse<TItem>, EditServiceResponse
    {
        public EditOkResponse(TItem original) : base(original) { }
    }

    public interface DeleteServiceResponse : ServiceResponse { }

    public record DeleteOkResponse : DeleteServiceResponse;
}