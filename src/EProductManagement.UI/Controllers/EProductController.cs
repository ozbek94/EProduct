using AutoMapper;
using EProductManagement.Domain.DTOs;
using EProductManagement.Domain.Entities;
using EProductManagement.Domain.Helpers;
using EProductManagement.Domain.Repositories;
using EProductManagement.Domain.Services;
using EProductManagement.UI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EProductManagement.UI.Controllers
{
    [Route("EProduct")]
    [ApiController]
    public class EProductController : ControllerBase
    {
        private readonly IEProductRepository _eProductRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IEProductService _eProductService;

        public EProductController(IEProductRepository eProductRepository, 
             IMapper mapper, 
             IConfiguration config,
             IEProductService eProductService)
        {
            _eProductRepository = eProductRepository;
            _mapper = mapper;
            _config = config;
            _eProductService = eProductService;
        }

        [HttpPost]
        public async Task<IActionResult> EProduct(EProductModel eproductCreationModel)
        {
            CustomHttpResponseMessage<PartyDTO> response = new CustomHttpResponseMessage<PartyDTO>();

            if (!HttpContext.User.DecideIfAdmin() && !HttpContext.User.DecideIfMerchantUser())
            {
                response.Success = false;
                response.ErrorMessage = "Yetkiniz yoktur.";
                return BadRequest(response.ErrorMessage);
            }



            EProduct eProduct = _mapper.Map<EProduct>(eproductCreationModel);

            if (HttpContext.User.DecideIfAdmin())
            {
                eProduct.IsApproved = true;
            }
            else
            {
                await _eProductService.AddMerchantId(eProduct);
                eProduct.IsApproved = false;
            }
            
            await _eProductRepository.CreateEProduct(eProduct);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> EProductById(int Id)
        {
            var eProduct = await _eProductRepository.GetByComfirmedEProductId(Id);
            var eProductModel = _mapper.Map<EProductModel>(eProduct);

            if (eProductModel != null)
            {
                return Ok(eProductModel);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("EProducts")]
        public async Task<IActionResult> EProducts()
        {
            var eProduct = await _eProductRepository.GetEProducts();
            var eProductModel = _mapper.Map<List<EProductModel>>(eProduct);

            if (eProductModel != null)
            {
                return Ok(eProductModel);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("ByMerchant")]
        public async Task<IActionResult> EProductByMerchantId(int MerchantId)
        {
            var eProduct = await _eProductRepository.GetByMerchantId(MerchantId);
            var eProductModel = _mapper.Map<List<EProductModel>>(eProduct);

            if (eProductModel != null)
            {
                return Ok(eProductModel);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut("ConfirmEProduct")]
        public async Task<IActionResult> ConfirmEProductById(EProductComfirmModel eProductComfirmModel)
        {
            CustomHttpResponseMessage<PartyDTO> response = new CustomHttpResponseMessage<PartyDTO>();

            if (!HttpContext.User.DecideIfAdmin())
            {
                response.Success = false;
                response.ErrorMessage = "Yetkiniz yoktur.";
                return BadRequest(response.ErrorMessage);
            };

            var eProduct = await _eProductRepository.GetByEProductId(eProductComfirmModel.Id);

            eProduct.IsApproved = eProductComfirmModel.Decision;
            
            if (eProductComfirmModel != null)
            {
                await _eProductRepository.UpdateEproduct(eProduct);
                return Ok(eProductComfirmModel);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEProductById(int Id)
        {
            CustomHttpResponseMessage<PartyDTO> response = new CustomHttpResponseMessage<PartyDTO>();

            if (!HttpContext.User.DecideIfAdmin() || !HttpContext.User.DecideIfMerchantUser())
            {
                response.Success = false;
                response.ErrorMessage = "Yetkiniz yoktur.";
                return BadRequest(response.ErrorMessage);
            };

            await _eProductRepository.DeleteEproduct(Id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEProductById(EProductModel eProductModel)
        {
            CustomHttpResponseMessage<PartyDTO> response = new CustomHttpResponseMessage<PartyDTO>();

            if (!HttpContext.User.DecideIfAdmin() && !HttpContext.User.DecideIfMerchantUser())
            {
                response.Success = false;
                response.ErrorMessage = "Yetkiniz yoktur.";
                return BadRequest(response.ErrorMessage);
            };

            EProduct eProduct = _mapper.Map<EProduct>(eProductModel);
            await _eProductRepository.UpdateEproduct(eProduct);
            return Ok();
        }

    }
}
