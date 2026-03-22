using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Entities;

public class Sport
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}
