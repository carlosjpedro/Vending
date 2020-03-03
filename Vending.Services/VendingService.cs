using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vending.Contracts.Exceptions;
using Vending.Contracts.Interfaces;
using Vending.Contracts.Model;

namespace Vending.Services
{
    public class VendingService : IVendingService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICashRegister _cashRegister;
        private readonly IWorkflowRepository _workflowRepository;

        public VendingService(
            IProductRepository productRepository,
            ICashRegister cashRegister,
            IWorkflowRepository workflowRepository)
        {
            _productRepository = productRepository;
            _cashRegister = cashRegister;
            _workflowRepository = workflowRepository;
        }

        public async Task<int> DepositCurrency(IEnumerable<CoinStack> coinStacks)
        {
            await _cashRegister.AddCoins(coinStacks);

            return await _workflowRepository.AddWorkflowSet(new PurchaseWorkflowStep
            {
                WorkflowId = Guid.NewGuid(),
                Balance = coinStacks.Sum(x => x.Count * x.Value),
                TimeStamp = DateTime.UtcNow,
                Status = PurchaseStatus.MoneyDeposited

            });
        }

        public Task<IEnumerable<Product>> AvailableProducts()
        {
            return _productRepository.GetAll();
        }

        public async Task<IEnumerable<CoinStack>> PurchaseProduct(int workflowStepId, int productId)
        {
            var workflowStep = await _workflowRepository.GetWorkflowStep(workflowStepId);
            var product = await _productRepository.GetById(productId);

            if (workflowStep.Balance < product.Price)
            {
                throw new InsufficientFounds();
            }

            await _productRepository.UpdateProduct(new Product(productId, product.Name, product.Price, product.Portions - 1));
            var change = workflowStep.Balance - product.Price;
            var changeCoins = await _cashRegister.GetCoins(change);

            await _workflowRepository.AddWorkflowSet(new PurchaseWorkflowStep
            {
                Balance = 0,
                ProductId = productId,
                Change = change,
                Status = PurchaseStatus.Fulfilled,
                TimeStamp = DateTime.UtcNow,
                WorkflowId = workflowStep.WorkflowId

            });

            return changeCoins;
        }

        public async Task<decimal> GetBalance(int lastStepId)
        {
            var workflowStep = await _workflowRepository.GetWorkflowStep(lastStepId);
            return workflowStep.Balance;
        }

        public async Task<IEnumerable<CoinStack>> Cancel(int lastStepId)
        {
            var workflowStep = await _workflowRepository.GetWorkflowStep(lastStepId);
            var change = await _cashRegister.GetCoins(workflowStep.Balance);


            await _workflowRepository.AddWorkflowSet(new PurchaseWorkflowStep
            {
                WorkflowId = workflowStep.WorkflowId,
                Balance = 0,
                Change = workflowStep.Balance,
                Status = PurchaseStatus.Canceled,
                TimeStamp = DateTime.UtcNow
            });

            return change;

        }
    }
}