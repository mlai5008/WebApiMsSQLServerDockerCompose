using Microsoft.EntityFrameworkCore;
using WebApiApp.Models;

namespace WebApiApp.Data
{
    public class PatientDbContext : DbContext
    {
        #region Ctor
        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options) { }
        #endregion

        #region Property
        public DbSet<Patient> Patient { get; set; } = default!;
        #endregion
    }
}
