using PDL.Synthetical.Domain;
using System.Collections;
using System.Collections.Generic;

namespace PDL.Synthetical.Services
{
    public interface IUserInfoService
    {
        void Add(UserInfo entity);
        void Delete(UserInfo entity);
        void Update(UserInfo entity);
        UserInfo GetById(int Id);

        IEnumerable<UserInfo> GetPageList(int pageIndex, int pageSize, out long rowsCount, object predicate = null, bool buffer = true);
    }
}