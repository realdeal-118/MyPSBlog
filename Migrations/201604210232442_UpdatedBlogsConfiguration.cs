namespace MyPSBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedBlogsConfiguration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Blogs", "AuthorId");
            AddForeignKey("dbo.Blogs", "AuthorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Blogs", new[] { "AuthorId" });
            DropColumn("dbo.Blogs", "AuthorId");
        }
    }
}
