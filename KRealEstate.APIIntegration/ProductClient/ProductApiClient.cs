using KRealEstate.Utilities.Constants;
using KRealEstate.ViewModels.Catalog.Product;
using KRealEstate.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;

namespace KRealEstate.APIIntegration.ProductClient
{
    public class ProductApiClient : BaseAPIClient, IProductApiClient
    {
        public ProductApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<bool> Create(PostProductRequest request)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseUrl.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SystemConstants.Authentication.RequestHeader, session);
            var requestContent = new MultipartFormDataContent();
            if (request.ThumbnailImages != null)
            {
                byte[] data;
                foreach (var item in request.ThumbnailImages)
                {
                    using (var br = new BinaryReader(item.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)item.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);
                    requestContent.Add(bytes, "thumbnailImages", item.FileName);
                }
                requestContent.Add(new StringContent(request.Name), "name");
                requestContent.Add(new StringContent(request.Price.ToString()), "price");
                requestContent.Add(new StringContent(request.Area.ToString()), "area");
                requestContent.Add(new StringContent(request.DirectionId), "directionId");
                requestContent.Add(new StringContent(request.ToletRoom.ToString()), "toletRoom");
                requestContent.Add(new StringContent(request.Bedroom.ToString()), "bedRoom");
                requestContent.Add(new StringContent(request.Description), "description");
                requestContent.Add(new StringContent(request.IsShowWeb.ToString()), "isShowWeb");
                requestContent.Add(new StringContent(request.Floor.ToString()), "floor");
                foreach (var item in request.CategoryId)
                {
                    requestContent.Add(new StringContent(item.ToString()), "categoryId");
                }

                requestContent.Add(new StringContent(request.Project.ToString()), "project");
                requestContent.Add(new StringContent(request.AddressDisplay), "addressDisplay");
                requestContent.Add(new StringContent(request.Furniture), "furniture");
                requestContent.Add(new StringContent(request.ProviceCode), "provinceCode");
                requestContent.Add(new StringContent(request.DistrictCode), "districtCode");
                requestContent.Add(new StringContent(request.WardCode), "wardCode");
                requestContent.Add(new StringContent(request.NameContact), "madeIn");
                requestContent.Add(new StringContent(request.PhoneContact), "madeIn");
                requestContent.Add(new StringContent(request.AddressContact), "madeIn");
                requestContent.Add(new StringContent(request.EmailContact), "madeIn");
                requestContent.Add(new StringContent(request.DayLengthPost.ToString()), "madeIn");
                requestContent.Add(new StringContent(request.MadeIn), "madeIn");
                requestContent.Add(new StringContent(request.MadeIn), "madeIn");
                requestContent.Add(new StringContent(request.MadeIn), "madeIn");
                requestContent.Add(new StringContent(request.MadeIn), "madeIn");
                requestContent.Add(new StringContent(request.MadeIn), "madeIn");
                var response = await client.PostAsync($"/api/products/", requestContent);
                return response.IsSuccessStatusCode;
            }

        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResult<ProductViewModel>> GetAll(PagingProduct request)
        {
            return await GetAsync<PageResult<ProductViewModel>>($"/api/products?pageIndex={request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}" +
                $"categoryId={request.CategoryId}&directionId={request.DirectionId}&priceFrom={request.PriceFrom}&priceTo={request.PriceTo}");
        }

        public Task<ProductViewModel> GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
