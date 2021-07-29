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
        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<VehicleEntity> VehicleEntity { get; set; }
        public DbSet<BrandEntity> BrandEntity { get; set; }
        public DbSet<TypeEntity> TypeEntity { get; set; }
        public DbSet<ModelEntity> ModelEntity { get; set; }
    }
}
