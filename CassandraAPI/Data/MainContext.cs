using Microsoft.EntityFrameworkCore;
using CassandraAPI.Models;

namespace CassandraAPI.Data
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options) : base(options)
        {
        }
        // เวลาสร้าง model Entity ใหม่ มาใส่ในนี้ด้วย
        //แบบนี้
        public DbSet<CarbonHistoryEntity> carbonHistoryEntity { get; set; }
        public DbSet<CarbonPerDayEntity> carbonPerDayEntity { get; set; }
        public DbSet<CarbonPerHourEntity> carbonPerHourEntity { get; set; }
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
