using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace RLTechnologies.Module.Banner.Migrations.EntityBuilders
{
    public class BannerEntityBuilder : AuditableBaseEntityBuilder<BannerEntityBuilder>
    {
        private const string _entityTableName = "RLTechnologiesBanner";
        private readonly PrimaryKey<BannerEntityBuilder> _primaryKey = new("PK_RLTechnologiesBanner", x => x.BannerId);
        private readonly ForeignKey<BannerEntityBuilder> _moduleForeignKey = new("FK_RLTechnologiesBanner_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public BannerEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override BannerEntityBuilder BuildTable(ColumnsBuilder table)
        {
            BannerId = AddAutoIncrementColumn(table,"BannerId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            BannerTitle = AddMaxStringColumn(table, "BannerTitle");
            BannerSubTitle = AddMaxStringColumn(table, "BannerSubTitle");
            BannerUrl = AddMaxStringColumn(table, "BannerUrl");
            BannerText = AddMaxStringColumn(table, "BannerText");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> BannerId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
        public OperationBuilder<AddColumnOperation> BannerTitle { get; set; }
        public OperationBuilder<AddColumnOperation> BannerSubTitle { get; set; }
        public OperationBuilder<AddColumnOperation> BannerUrl { get; set; }
        public OperationBuilder<AddColumnOperation> BannerText { get; set; }
    }
}
