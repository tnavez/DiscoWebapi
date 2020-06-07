using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Disco.Entities;
using Disco.Models;
using Disco.Repository.Contract;
using Disco.Validator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Disco.Controllers
{
    [Route("api/Libraries")]
    [ApiController]
    public class DiscoController : ControllerBase
    { 
        private readonly IDiscoRepository<Member> _discoRepository;

        public DiscoController(IDiscoRepository<Member> discoRepository)
        {
            _discoRepository = discoRepository;
        }

        [HttpGet]
        [Route("GetAllMember")]
        public async Task<IActionResult> GetAllMember()
        {
            var members = await _discoRepository.GetAllMember();
            return Ok(members);
        }

        [HttpGet]
        [Route("GetMember")]
        public async Task<IActionResult> GetMember(int MemberId)
        {
            try
            {
                var member = await _discoRepository.GetMember(MemberId);
                return Ok(member);
            }
            catch (Exception ex)
            {
                //log exception   
                return BadRequest();
            }
        }





        [HttpGet]
        [Route("BlacklistCheck")]
        public async Task<IActionResult> BlacklistCheck(int MemberId)
        {
            try
            {
                BlackListLog mem = await _discoRepository.BlacklistCheck(MemberId);
      
                if(mem != null)
                {
                    return Ok("Member blacklisted");
                }
                else
                {
                    return Ok("Member allowed");
                }
            }
            catch (Exception ex)
            {
                //log exception   
                return BadRequest();
            }
        }
        
        [HttpPut]
        [Route("UpdateMember")]
        public async Task<IActionResult> UpdateMember(JObject objData)
        {
            dynamic jsonData = objData;

            StringBuilder sb = new StringBuilder();
            try
            {
                JObject identityCard = jsonData.IdentityCard;
                JObject member = jsonData.Member;

                
                var memberM = member.ToObject<Member>();



                var validatorm = new MemberValidator();
                var resultv = await validatorm.ValidateAsync(memberM);

                if (resultv != null)
                {
                    sb.Append(string.Join(",", resultv));
                }

                var identityCardM = identityCard.ToObject<IdentityCard>();
                var validatoric = new IdentityCardValidator();
                var result = await validatoric.ValidateAsync(identityCardM);
               
                if(result != null)
                {
                    sb.Append(string.Join(",", result));
                }

                if(sb.Length >0)
                {
                    return BadRequest("Errors: " + sb.ToString());
                }

                await _discoRepository.UpdateMember(memberM, identityCardM);
                return Ok("Member updated");
            }
            catch (Exception ex)
            {
                //log Exception  
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("CreateMember")]
        public async Task<IActionResult> CreateMember(JObject objData)
        {
            dynamic jsonData = objData;

            try
            {
                JObject identityCard = jsonData.IdentityCard;
                JObject member = jsonData.Member;

                StringBuilder sb = new StringBuilder();


                var identityCardM = identityCard.ToObject<IdentityCard>();
                
                var memberM = member.ToObject<Member>();

                var validatorm = new MemberValidator();
                var resultv = await validatorm.ValidateAsync(memberM);

                if (resultv != null)
                {
                    sb.Append(string.Join(",", resultv));
                }

                var validatoric = new IdentityCardValidator();
                var result = await validatoric.ValidateAsync(identityCardM);

                if (result != null)
                {
                    sb.Append(string.Join(",", result));
                }

                if (sb.Length > 0)
                {
                    return BadRequest("Errors: " + sb.ToString());
                }




                await _discoRepository.AddMember(memberM, identityCardM);

                return Ok(member);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("BlackListMamber2")]
        public async Task<IActionResult> BlackListMemver2(int MemberId, int days)
        {
            try
            {
                await _discoRepository.BlackListMember(MemberId, days);
                return Ok("MemberId " + MemberId.ToString() + " has been blacklister for " + days.ToString() + " days");
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }





        /*
        [HttpPost]
        [Route("CreateMember")]
        public async Task<IActionResult> CreateMember(JObject objData)
        {
            dynamic jsonData = objData;
            
            try
            {
                JObject identityCard = jsonData.IdentityCard;
                JObject member = jsonData.Member;

                StringBuilder sb = new StringBuilder();


                var identityCardM = identityCard.ToObject<IdentityCard>();
                var memberM = member.ToObject<Member>();

                string Errors = Tools.CheckData(identityCardM, memberM);
                if (Errors.Length > 0)
                {
                    return BadRequest("Error(s):\n" + Errors);
                }

                await _discoRepository.AddMember(memberM, identityCardM);

                return Ok(member);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }*/
    }
}