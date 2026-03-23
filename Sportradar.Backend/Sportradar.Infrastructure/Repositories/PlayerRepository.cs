using Microsoft.EntityFrameworkCore;
using Sportradar.Core.Application.ServiceContracts;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Sportradar.Infrastructure.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly ApplicationDbContext _context;
    public PlayerRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(Player player)
    {
        if ((await _context.Players.FirstOrDefaultAsync(p => p.Id == player.Id)) == null)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        Player? player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
        if (player == null) return;
        _context.Players.Remove(player);
        await _context.SaveChangesAsync();
    }

    public async Task<Player?> GetByIdAsync(Guid id)
    {
        return await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<List<Player>> GetAll() => await _context.Players.ToListAsync();

}
