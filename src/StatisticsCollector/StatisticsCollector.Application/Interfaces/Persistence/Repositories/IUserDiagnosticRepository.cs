using StatisticsCollector.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsCollector.Application.Interfaces.Persistence.Repositories;

public interface IUserDiagnosticRepository : IDbEntityBaseRepository<UserDiagnostic>
{
}
