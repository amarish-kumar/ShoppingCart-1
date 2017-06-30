using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart.Migrations
{
    public partial class SeedFeaturesDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO FEATURES (Name) VALUES ('Feature 1')");
            migrationBuilder.Sql("INSERT INTO FEATURES (Name) VALUES ('Feature 2')");
            migrationBuilder.Sql("INSERT INTO FEATURES (Name) VALUES ('Feature 3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM FEATURES");
        }
    }
}
