﻿using Microsoft.AspNetCore.Http;
using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RealEstateDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IPropertyRepository _propertyRepository;
        public UnitOfWork(RealEstateDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
        }
        public IPropertyRepository PropertyRepository =>
            _propertyRepository ??= new PropertyRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst("uid")?.Value;

            await _context.SaveChangesAsync(username);
        }
    }
}