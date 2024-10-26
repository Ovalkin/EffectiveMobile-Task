using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using EffectiveMobile.Common.DTOs;
using EffectiveMobile.Common.EntityModel.Sqlite;
using EffectiveMobile.Tests.Integration.Factories;
using EffectiveMobile.Tests.Integration.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EffectiveMobile.Tests.Integration;

public class OrdersTest : IClassFixture<CustomWebApplicationFactory>
{
    public OrdersTest(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();

        var scope = factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        _context = serviceProvider.GetRequiredService<EffectiveMobileContext>();
    }
    private readonly HttpClient _client;
    private readonly EffectiveMobileContext _context;

    [Theory]
    [InlineData("POST")]
    public async Task CreateOrder_WithCreatedDataIsValid_ShouldReturnOk(string method)
    {
        var createdOrder = new CreateOrderDto
        {
            Weight = 18.9,
            CityDistrict = "Ленинский",
            DeliveryTime = DateTime.Parse("2024-10-26 17:45:32")
        };

        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Orders");
        request.Content = JsonContent.Create(createdOrder);

        var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        await SeadDataHelper.ClearDb(_context);
    }

    [Theory]
    [InlineData("POST")]
    public async Task CreateOrder_WithCreatedDataDateTimeIsNotValid_ShouldReturnBadRequest(string method)
    {
        var createdOrder = new CreateOrderDto
        {
            Weight = 18.9,
            CityDistrict = "Ленинский",
            DeliveryTime = DateTime.Parse("2014-10-26 17:32")
        };

        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Orders");
        request.Content = JsonContent.Create(createdOrder);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await SeadDataHelper.ClearDb(_context);
    }

    [Theory]
    [InlineData("POST")]
    public async Task CreateOrder_WithCreatedDataWeightIsNotValid_ShouldReturnBadRequest(string method)
    {
        var createdOrder = new CreateOrderDto
        {
            Weight = -18.9,
            CityDistrict = "Ленинский",
            DeliveryTime = DateTime.Parse("2024-10-26 17:32")
        };

        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Orders");
        request.Content = JsonContent.Create(createdOrder);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await SeadDataHelper.ClearDb(_context);
    }

    [Theory]
    [InlineData("POST")]
    public async Task CreateOrder_WithCreatedDataCityDistrictIsNotValid_ShouldReturnBadRequest(string method)
    {
        var createdOrder = new CreateOrderDto
        {
            Weight = 18.9,
            CityDistrict = "Ле888нинский",
            DeliveryTime = DateTime.Parse("2024-10-26 17:32")
        };

        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Orders");
        request.Content = JsonContent.Create(createdOrder);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await SeadDataHelper.ClearDb(_context);
    }

    [Theory]
    [InlineData("GET")]
    public async Task GetFiltered_WithParamsIsValid_ShouldReturnOk(string method)
    {
        await SeadDataHelper.Sead(_context);
        var cityDistrict = Uri.EscapeDataString("Ленинский");
        var firstDeliveryDateTime = Uri.EscapeDataString(DateTime.Now.ToString(CultureInfo.InvariantCulture));

        var request = new HttpRequestMessage(new HttpMethod(method),
            $"/api/Orders?cityDistrict={cityDistrict}&firstDeliveryDateTime={firstDeliveryDateTime}");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        await SeadDataHelper.ClearDb(_context);
    }
    
    [Theory]
    [InlineData("GET")]
    public async Task GetFiltered_WithParamsIsDateTimeNotValid_ShouldReturnBadRequest(string method)
    {
        await SeadDataHelper.Sead(_context);
        var cityDistrict = Uri.EscapeDataString("Ленинский");
        var firstDeliveryDateTime = Uri.EscapeDataString("2020-2-21 17:33:22");

        var request = new HttpRequestMessage(new HttpMethod(method),
            $"/api/Orders?cityDistrict={cityDistrict}&firstDeliveryDateTime={firstDeliveryDateTime}");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await SeadDataHelper.ClearDb(_context);
    }
    
    [Theory]
    [InlineData("GET")]
    public async Task GetFiltered_WithParamsIsPathNotValid_ShouldReturnBadRequest(string method)
    {
        await SeadDataHelper.Sead(_context);
        var cityDistrict = Uri.EscapeDataString("Ленинский");
        var firstDeliveryDateTime = Uri.EscapeDataString("2020-2-21 17:33:22");
        var path = Uri.EscapeDataString("/not/valid/directory");

        var request = new HttpRequestMessage(new HttpMethod(method),
            $"/api/Orders?cityDistrict={cityDistrict}&firstDeliveryDateTime={firstDeliveryDateTime}&path={path}");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await SeadDataHelper.ClearDb(_context);
    }
 }