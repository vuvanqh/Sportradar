using Microsoft.EntityFrameworkCore;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly ApplicationDbContext _context;
    public TeamRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<SportTeam>> GetAllAsync()
    {
        return await _context.SportTeams.ToListAsync();
    }

    public async Task<SportTeam?> GetByIdAsync(Guid teamId)
    {
        return await _context.SportTeams.FirstOrDefaultAsync(t => t.Id == teamId);
    }

    public async Task<List<SportTeam>> GetBySportAsync(Guid sportId)
    {
        return await _context.SportTeams.Where(t=>t.SportId==sportId).ToListAsync();
    }
    public async Task<bool> ExistsAsync(Guid teamId)
    {
        return await _context.SportTeams.AnyAsync(t => t.Id == teamId);
    }
}
