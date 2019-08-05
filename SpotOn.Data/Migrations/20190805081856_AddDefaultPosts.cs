using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotOn.Data.Migrations
{
    public partial class AddDefaultPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Posts(Id, Title, Body, CreatedAt, UpdatedAt, UserId, ImagePath) VALUES(NEWID(), 'Azure', 'Post on azure', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), (SELECT u.Id FROM Users u WHERE u.Username = 'John Doe' and u.Password = 'john$sign'), 'images/dummy-post-horisontal-thegem-blog-default.jpg');");

            migrationBuilder.Sql("INSERT INTO Posts(Id, Title, Body, CreatedAt, UpdatedAt, UserId, ImagePath) VALUES(NEWID(), 'C#', 'Post on C#', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), (SELECT u.Id FROM Users u WHERE u.Username = 'John Doe' and u.Password = 'john$sign'), 'images/dummy-post-horisontal-thegem-blog-default.jpg');");

            migrationBuilder.Sql("INSERT INTO Posts(Id, Title, Body, CreatedAt, UpdatedAt, UserId, ImagePath) VALUES(NEWID(), 'Entity Framework', 'Post on Entity Framework', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), (SELECT u.Id FROM Users u WHERE u.Username = 'John Doe' and u.Password = 'john$sign'), 'images/dummy-post-horisontal-thegem-blog-default.jpg');");

            migrationBuilder.Sql("INSERT INTO Posts(Id, Title, Body, CreatedAt, UpdatedAt, UserId, ImagePath) VALUES(NEWID(), 'Life at the beach', 'Durban beach, Most beautiful in the world!', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), (SELECT u.Id FROM Users u WHERE u.Username = 'John Doe' and u.Password = 'john$sign'), 'images/dummy-post-horisontal-thegem-blog-default.jpg');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
