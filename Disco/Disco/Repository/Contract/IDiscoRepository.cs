using Disco.Entities;
using Disco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.Repository.Contract
{
        public interface IDiscoRepository<T>
        {
            Task<IEnumerable<Member>> GetAllMember();
            Task<Member> GetMember(int MemberId);
            Task AddMember(Member member, IdentityCard identityCard);
            Task UpdateMember(Member member,IdentityCard identityCard);
            Task<BlackListLog> BlacklistCheck(int MemberId);
            Task BlackListMember(int MemberId, int Days);

        //Member BlacklistCheck(int MemberId);

    }
}
