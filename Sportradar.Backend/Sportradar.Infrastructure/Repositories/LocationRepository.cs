using Microsoft.EntityFrameworkCore;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly ApplicationDbContext _context;
    public LocationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(Location location)
    {
        if((await _context.Locations.FirstOrDefaultAsync(l=>l.Id==location.Id))==null)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        Location? locaiton = await _context.Locations.FirstOrDefaultAsync(l => l.Id == id);
        if (locaiton == null) return;
        _context.Locations.Remove(locaiton);
        await _context.SaveChangesAsync();
    }

    public async Task<Location?> GetByIdAsync(Guid id)
    {
        return await _context.Locations.FirstOrDefaultAsync(l => l.Id == id);
    }
    public async Task<List<Location>> GetAll()
    {
        return await _context.Locations.ToListAsync();
    }
}
