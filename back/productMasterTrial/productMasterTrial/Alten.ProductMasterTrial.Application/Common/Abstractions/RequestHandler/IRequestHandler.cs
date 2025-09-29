namespace Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler
{
    public interface IRequestHandler<in TRequest, TResult>
    {
        public Task<TResult> Handle(TRequest request);
    }

}
