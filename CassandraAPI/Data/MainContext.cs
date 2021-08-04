using Microsoft.EntityFrameworkCore;
using CassandraAPI.Models;

namespace CassandraAPI.Data
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CarbonHistoryEntity> carbonHistoryEntity { get; set; }
        public DbSet<CarbonRankingEntity> carbonRankingEntity { get; set; }
        public DbSet<HealthCheckEntity> healthCheckEntity { get; set; }
        public DbSet<OnlineTimeEntity> onlineTimeEntity { get; set; }
        public DbSet<PositionEntity> positionEntity { get; set; }
        public DbSet<UserEntity> userEntity { get; set; }
        public DbSet<VehicleEntity> vehicleEntity { get; set; }
        public DbSet<BrandEntity> brandEntity { get; set; }
        public DbSet<TypeEntity> typeEntity { get; set; }
        public DbSet<ModelEntity> modelEntity { get; set; }
        public DbSet<UserCarbonEntity> userCarbonEntity { get; set; }
    }
}
