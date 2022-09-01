using AutoMapper;
using EProductManagement.Domain.Entities;
using EProductManagement.Domain.Repositories;
using EProductManagement.UI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EProductManagement.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBalanceController : ControllerBase
    {
        private readonly IProductBalanceRepository _productBalanceRepository;
        private readonly IMapper _mapper;

        public ProductBalanceController
            (IProductBalanceRepository productBalanceRepository,
             IMapper mapper)
        {
            this._productBalanceRepository = productBalanceRepository;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> ProductBalance(ProductBalance productBalance)
        {

            await _productBalanceRepository.CreateProductBalance(productBalance);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductBalancesWithPartyId(int PartyId)
        {
            
            var ProductBalances = _mapper.Map<List<ProductBalanceModel>>(await _productBalanceRepository.GetProductBalancesByPartyId(PartyId));
            return Ok(ProductBalances);
        }

    }
}
