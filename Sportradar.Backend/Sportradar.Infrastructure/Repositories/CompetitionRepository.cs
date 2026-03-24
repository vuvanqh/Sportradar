using Microsoft.EntityFrameworkCore;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Infrastructure.Repositories;

public class CompetitionRepository : ICompetitionRepository
{
    private readonly ApplicationDbContext _context;
    public CompetitionRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Competition>> GetAllAsync()
    {
        return await _context.Competitions.ToListAsync();
    }

    public async Task<Competition?> GetByIdAsync(Guid competitionId)
    {
        return await _context.Competitions.FirstOrDefaultAsync(c=>c.Id==competitionId);
    }

    public async Task<List<Competition>> GetBySportIdAsync(Guid sportId)
    {
        return await _context.Competitions.Where(c => c.SportId == sportId).ToListAsync();
    }
}
