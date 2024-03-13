using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ResponseDto _response;
        private IMapper _mapper;
        public CouponAPIController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _response = new ResponseDto();
            _mapper = mapper;
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _context.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
            }catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon obj = _context.Coupons.First(c=>c.CouponId==id);
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch(Exception ex)
            {
                _response.IsSuccess =false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _context.Coupons.First(c => c.CouponCode == code);
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch(Exception e)
            {
                _response.IsSuccess =false;
                _response.Message = e.Message;
            }
            return _response;
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _context.Coupons.Add(obj);
                _context.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _context.Coupons.Update(obj);
                _context.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess =false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _context.Coupons.First(c => c.CouponId == id);
                _context.Coupons.Remove(obj);
                _context.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess =false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
