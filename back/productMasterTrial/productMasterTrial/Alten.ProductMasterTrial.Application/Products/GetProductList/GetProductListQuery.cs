namespace Alten.ProductMaster.Application.Products.GetProductList
{
    public sealed record GetProductListQuery(int pageNumber = 1, int pageSize = 10);
}
