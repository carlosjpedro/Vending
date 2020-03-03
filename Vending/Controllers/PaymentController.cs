using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vending.Contracts.Model;
using Vending.Models;
using Vending.Services;

namespace Vending.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IVendingService _vendingService;
        private readonly ICashRegister _cashRegister;
        private readonly IMapper _mapper;

        public PaymentController(IVendingService vendingService, ICashRegister cashRegister, IMapper mapper)
        {
            _vendingService = vendingService;
            _cashRegister = cashRegister;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var currencies = await _cashRegister.AvailableCurrencies();
            var model = _mapper.Map<PaymentViewModel>(currencies);
            return View(model);
        }

        public async Task<IActionResult> Pay(PaymentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", viewModel);
            }

            var currency = _mapper.Map<IEnumerable<CoinStack>>(viewModel);

            var lastStepId = await _vendingService.DepositCurrency(currency);
            return RedirectToAction("Index", "Product", new { lastStepId });
        }
    }
}