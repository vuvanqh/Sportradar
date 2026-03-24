using Microsoft.EntityFrameworkCore;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Infrastructure.Repositories;

public class SportRepository : ISportRepository
{
    private readonly ApplicationDbContext _context;
    public SportRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Sport>> GetAllAsync()
    {
        return await _context.Sports.ToListAsync();
    }
}
