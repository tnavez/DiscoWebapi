using Disco.Entities;
using Disco.Models;
using Disco.Repository.Contract;
using Disco.Validator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Disco.Repository.Implementation
{
    public class DiscoRepository : IDiscoRepository<Member>
    {
        readonly DiscoContext _DiscoContext;

        public DiscoRepository(DiscoContext context)
        {
            _DiscoContext = context;
        }

        public async Task<IEnumerable<Member>> GetAllMember()
        {
            return await _DiscoContext.Members.ToListAsync();
        }

        public async Task<Member> GetMember(int MemberId)
        {
            try
            {
                return await _DiscoContext.Members.Where(e => e.MemberId == MemberId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                //log exception  
                return null;
            }
        }

        public async Task AddMember(Member member, IdentityCard identityCard)
        {
            try
            {
                if (_DiscoContext != null)
                {
                    if (!CheckIdCard(member))
                    {
                        IdentityCard oldIdentityCard = _DiscoContext.IdentityCards.Where(e => e.MemberId == member.MemberId).FirstOrDefault();

                        oldIdentityCard.CardNumber = identityCard.CardNumber;
                        oldIdentityCard.ExpirDate = identityCard.ExpirDate;
                        oldIdentityCard.ValiDate = identityCard.ValiDate;
                        _DiscoContext.Update(member);
                        await _DiscoContext.SaveChangesAsync();
                    }
                    if(!CheckCard(member))
                    {
                        Cards cards = new Cards();

                        cards.ExpirDate = DateTime.Now.AddDays(365);
                        cards.ValidFrom = DateTime.Now;

                        List<Cards> _lc = _DiscoContext.Cards.Where(e => e.Member == member).ToList();
                        _lc.Add(cards);

                        member.Cards = _lc;
                        _DiscoContext.Update(member);
                        await _DiscoContext.SaveChangesAsync();

                    }
                    else
                    {
                        Cards cards = new Cards();

                        cards.ExpirDate = DateTime.Now.AddDays(365);
                        cards.ValidFrom = DateTime.Now;

                        List<Cards> _lc = new List<Cards>();
                        _lc.Add(cards);

                        member.Cards = _lc;
                        member.Identity = identityCard;

                        _DiscoContext.Add(member);

                        await _DiscoContext.SaveChangesAsync();
                    }

                }
            }
            catch(Exception e)
            { }
        } 

        public async Task UpdateMember(Member member, IdentityCard identityCard)
        {

            try
            { 
            var Memberupd = await _DiscoContext.Members.SingleOrDefaultAsync(m => m.MemberId == member.MemberId);
            var idupd = await _DiscoContext.IdentityCards.SingleOrDefaultAsync(m => m.MemberId == member.MemberId);

                if (Memberupd != null)
            {
                Memberupd.PhoneNumber = member.PhoneNumber;
                Memberupd.Email = member.Email;

                if (Tools.CheckMinAge(identityCard.BirthDate) && Tools.IsValidNN(identityCard.NatNumber))
                {
                        identityCard.CardNumber = identityCard.CardNumber;
                        idupd.FirstName = identityCard.FirstName;
                        idupd.LastName = identityCard.LastName;
                        idupd.BirthDate = identityCard.BirthDate;
                        idupd.ValiDate = identityCard.ValiDate;
                        idupd.ExpirDate = identityCard.ExpirDate;
                        _DiscoContext.Update(idupd);
                    }
                _DiscoContext.Update(Memberupd);
                await _DiscoContext.SaveChangesAsync();

            }
        }
        catch (Exception ex)
            {
                //log exception  
               // return null;
            }

}



        public async Task<BlackListLog> BlacklistCheck(int MemberId)
        {
            try
            {
                BlackListLog BlacLlog = await _DiscoContext.BlackListLogs.Where(e => e.Member.MemberId == MemberId && e.BlackListDate > DateTime.Now).OrderByDescending(e=>e.BlackId).FirstOrDefaultAsync(); //.OrderByDescending(e=>e.BlackId).FirstOrDefaultAsync();

                if (BlacLlog != null)
                {
                    if (BlacLlog.BlackListDate > DateTime.Now)
                    {
                        return BlacLlog;
                    }

                }
                return null;

            }
            catch (Exception ex)
            {
                //log exception  
                return null;
            }


        }



        public async Task BlackListMember(int MemberId, int Days)
        {
            if (_DiscoContext != null)
            {
                try
                {
                    Member member = await _DiscoContext.Members.Where(e => e.MemberId == MemberId).FirstOrDefaultAsync();

                    BlackListLog blackListLog = new BlackListLog();

                    blackListLog.BlackListDate = DateTime.Now.AddDays(Days);

                    List<BlackListLog> _lb = new List<BlackListLog>();
                    _lb.Add(blackListLog);

                    member.BlackListLogs = _lb;

                    _DiscoContext.Update(member);

                    await _DiscoContext.SaveChangesAsync();
                }
                catch(Exception e)
                {
                   // return null;
                }
             }

        }


        public Boolean CheckIdCard(Member member)
        {

            IdentityCard identityCard = _DiscoContext.IdentityCards.Where(e => e.ExpirDate > DateTime.Now || e.ValiDate <= DateTime.Now && e.Member.MemberId == member.MemberId).FirstOrDefault();

            if(identityCard != null)
            {
                return false;
            }
            return true;
        }

        public Boolean CheckCard(Member member)
        {
                Cards cards = _DiscoContext.Cards.Where(e=>e.Member.MemberId == member.MemberId).OrderByDescending(f=>f.CardId).FirstOrDefault();
                    
                if(cards != null)
                {
                    if (cards.ExpirDate > DateTime.Now || cards.ValidFrom <= DateTime.Now || cards.Lost == true)
                    {
                        return false;
                    }
                }
                return true;
                
        }
    }
    
}
