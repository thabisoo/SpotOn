using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotOn.Data.Migrations
{
    public partial class AddDefaultUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Users(Id, Email, Password, Username, CreatedAt, UpdatedAt) VALUES(NEWID(), 'john.doe@gmail.com', 'john$sign', 'John Doe', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET());");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
