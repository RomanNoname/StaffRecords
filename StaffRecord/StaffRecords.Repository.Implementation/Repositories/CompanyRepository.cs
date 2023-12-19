﻿using StaffRecords.DatainItialisation;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Entities;

namespace StaffRecords.Repository.Implementation.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(ConnectionInfo connectionInfo):base(connectionInfo) { }
   
    }
}
