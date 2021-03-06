﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.API.Extensions;
using Store.API.ViewModels;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.BusinessLayer.Responses;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    public class SalesController : Controller
    {
        protected ILogger Logger;
        protected IHumanResourcesBusinessObject HumanResourcesBusinessObject;
        protected IProductionBusinessObject ProductionBusinessObject;
        protected ISalesBusinessObject SalesBusinessObject;

        public SalesController(ILogger<SalesController> logger, IHumanResourcesBusinessObject humanResourcesBusinessObject, IProductionBusinessObject productionBusinessObject, ISalesBusinessObject salesBusinessObject)
        {
            Logger = logger;
            HumanResourcesBusinessObject = humanResourcesBusinessObject;
            ProductionBusinessObject = productionBusinessObject;
            SalesBusinessObject = salesBusinessObject;
        }

        protected override void Dispose(Boolean disposing)
        {
            SalesBusinessObject?.Dispose();

            base.Dispose(disposing);
        }

        [HttpGet]
        [Route("Order")]
        public async Task<IActionResult> GetOrdersAsync(Int32? pageSize = 10, Int32? pageNumber = 1)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetOrdersAsync));

            var response = await SalesBusinessObject.GetOrdersAsync((Int32)pageSize, (Int32)pageNumber);

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("Order/{id}")]
        public async Task<IActionResult> GetOrderAsync(Int32 id)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetOrderAsync));

            var response = await SalesBusinessObject.GetOrderAsync(id);

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("CreateOrderViewModel")]
        public async Task<IActionResult> GetCreateOrderRequestAsync()
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetCreateOrderRequestAsync));

            var response = new SingleModelResponse<CreateOrderViewModel>() as ISingleModelResponse<CreateOrderViewModel>;

            var customersResponse = await SalesBusinessObject.GetCustomersAsync();

            response.Model.Customers = customersResponse.Model.Select(item => new CustomerViewModel(item));

            var employeesResponse = await HumanResourcesBusinessObject.GetEmployeesAsync();

            response.Model.Employees = employeesResponse.Model.Select(item => new EmployeeViewModel(item));

            var shippersResponse = await SalesBusinessObject.GetShippersAsync();

            response.Model.Shippers = shippersResponse.Model.Select(item => new ShipperViewModel(item));

            var productsResponse = await ProductionBusinessObject.GetProductsAsync();

            response.Model.Products = productsResponse.Model.Select(item => new ProductViewModel(item));

            return response.ToHttpResponse();
        }

        [HttpPost]
        [Route("Order")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderViewModel value)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(CreateOrderAsync));

            var response = await SalesBusinessObject.CreateOrderAsync(value.GetOrder(), value.GetOrderDetails().ToArray());

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("CloneOrder/{id}")]
        public async Task<IActionResult> CloneOrderAsync(Int32 id)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(CloneOrderAsync));

            var response = await SalesBusinessObject.CloneOrderAsync(id);

            return response.ToHttpResponse();
        }

        [HttpDelete]
        [Route("Order/{id}")]
        public async Task<IActionResult> RemoveOrderAsync(Int32 id)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(RemoveOrderAsync));

            var response = await SalesBusinessObject.RemoveOrderAsync(id);

            return response.ToHttpResponse();
        }
    }
}
