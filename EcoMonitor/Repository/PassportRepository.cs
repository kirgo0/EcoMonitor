﻿using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;

namespace EcoMonitor.Repository
{
    public class PassportRepository : Repository<Passport>, IPassportRepository
    {
        public PassportRepository(ApplicationDbContext db) : base(db)
        {
        }

    }
}
