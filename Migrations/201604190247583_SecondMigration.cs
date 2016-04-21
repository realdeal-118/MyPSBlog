namespace MyPSBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "PostId" });
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                        Title = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        MediaUrl = c.String(),
                        Publish = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Comments", "BlogId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "CreatedOn", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Comments", "BlogId");
            AddForeignKey("dbo.Comments", "BlogId", "dbo.Blogs", "Id", cascadeDelete: true);
            DropColumn("dbo.Comments", "PostId");
            DropColumn("dbo.Comments", "Create");
            DropTable("dbo.Posts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        Updated = c.DateTimeOffset(precision: 7),
                        Title = c.String(),
                        Body = c.String(),
                        MediaUrl = c.String(),
                        Publish = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Comments", "Create", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Comments", "PostId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "BlogId", "dbo.Blogs");
            DropIndex("dbo.Comments", new[] { "BlogId" });
            DropColumn("dbo.Comments", "CreatedOn");
            DropColumn("dbo.Comments", "BlogId");
            DropTable("dbo.Blogs");
            CreateIndex("dbo.Comments", "PostId");
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
    }
}
