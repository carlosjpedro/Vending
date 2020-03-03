using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vending.Contracts.Exceptions;
using Vending.Models;
using Vending.Services;

namespace Vending.Controllers
{
    public class ProductController : Controller
    {
        private readonly IVendingService _vendingService;
        private readonly IMapper _mapper;

        public ProductController(IVendingService vendingService, IMapper mapper)
        {
            _vendingService = vendingService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int lastStepId, string errorMessage = null)
        {
            var products = await _vendingService.AvailableProducts();
            var balance = await _vendingService.GetBalance(lastStepId);
            var productViewModel = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            var productSelectionViewModel = new ProductSelectionViewModel
            {
                Products = productViewModel,
                WorkflowStepId = lastStepId,
                Balance = balance,
                ErrorMessage = errorMessage
            };
            return View(productSelectionViewModel);
        }

        public async Task<IActionResult> Select(SelectedProductViewModel viewModel)
        {
            try
            {
                var change = await _vendingService.PurchaseProduct(viewModel.WorkflowStepId, viewModel.ProductId);
                var model = _mapper.Map<PaymentViewModel>(change);
                return View("Completed", model);
            }
            catch (VendingException e)
            {
                return RedirectToAction("Index", new { lastStepId = viewModel.WorkflowStepId, errorMessage = e.Message });
            }

        }

        public async Task<IActionResult> Cancel(int workflowStepId)
        {
            var change = await _vendingService.Cancel(workflowStepId);
            var model = _mapper.Map<PaymentViewModel>(change);
            return View("Completed", model);
        }
    }
}