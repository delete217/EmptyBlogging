using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDemo.API.Entity;

public partial class BlogContext : DbContext
{
    public BlogContext()
    {
    }

    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MsAdmin> MsAdmins { get; set; }

    public virtual DbSet<MsAdminPermission> MsAdminPermissions { get; set; }

    public virtual DbSet<MsArticle> MsArticles { get; set; }

    public virtual DbSet<MsArticleBody> MsArticleBodies { get; set; }

    public virtual DbSet<MsArticleTag> MsArticleTags { get; set; }

    public virtual DbSet<MsCategory> MsCategories { get; set; }

    public virtual DbSet<MsComment> MsComments { get; set; }

    public virtual DbSet<MsPermission> MsPermissions { get; set; }

    public virtual DbSet<MsSysLog> MsSysLogs { get; set; }

    public virtual DbSet<MsSysUser> MsSysUsers { get; set; }

    public virtual DbSet<MsTag> MsTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=blog;uid=root;pwd=123456;port=3306", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<MsAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("ms_admin")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
            entity.Property(e => e.Avatar)
                .HasMaxLength(100).HasColumnName("avatar");
        });

        modelBuilder.Entity<MsAdminPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("ms_admin_permission")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
        });

        modelBuilder.Entity<MsArticle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("ms_article")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId)
                .HasComment("作者id")
                .HasColumnName("author_id");
            entity.Property(e => e.BodyId)
                .HasComment("内容id")
                .HasColumnName("body_id");
            entity.Property(e => e.CategoryId)
                .HasComment("类别id")
                .HasColumnName("category_id");
            entity.Property(e => e.CommentCounts)
                .HasComment("评论数量")
                .HasColumnName("comment_counts");
            entity.Property(e => e.CreateDate)
                .HasComment("创建时间")
                .HasColumnName("create_date");
            entity.Property(e => e.Summary)
                .HasMaxLength(255)
                .HasComment("简介")
                .HasColumnName("summary");
            entity.Property(e => e.Title)
                .HasMaxLength(64)
                .HasComment("标题")
                .HasColumnName("title");
            entity.Property(e => e.ViewCounts)
                .HasComment("浏览数量")
                .HasColumnName("view_counts");
            entity.Property(e => e.Weight)
                .HasComment("是否置顶")
                .HasColumnName("weight");
        });

        modelBuilder.Entity<MsArticleBody>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("ms_article_body")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.ArticleId, "article_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.ContentHtml).HasColumnName("content_html");
        });

        modelBuilder.Entity<MsArticleTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("ms_article_tag")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.ArticleId, "article_id");

            entity.HasIndex(e => e.TagId, "tag_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
        });

        modelBuilder.Entity<MsCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("ms_category")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .HasColumnName("avatar")
                .UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name")
                .UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description")
                .UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");
        });

        modelBuilder.Entity<MsComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("ms_comment")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.ArticleId, "article_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content")
                .UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.CreateDate).HasColumnName("create_date");
            entity.Property(e => e.Level)
                .HasMaxLength(1)
                .HasColumnName("level");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.ToUid).HasColumnName("to_uid");
        });

        modelBuilder.Entity<MsPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("ms_permission")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Path)
                .HasMaxLength(255)
                .HasColumnName("path");
        });

        modelBuilder.Entity<MsSysLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("ms_sys_log")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_unicode_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDate).HasColumnName("create_date");
            entity.Property(e => e.Ip)
                .HasMaxLength(15)
                .HasColumnName("ip")
                .UseCollation("utf8mb3_bin");
            entity.Property(e => e.Method)
                .HasMaxLength(100)
                .HasColumnName("method")
                .UseCollation("utf8mb3_bin");
            entity.Property(e => e.Module)
                .HasMaxLength(10)
                .HasColumnName("module")
                .UseCollation("utf8mb3_bin");
            entity.Property(e => e.Nickname)
                .HasMaxLength(10)
                .HasColumnName("nickname")
                .UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Operation)
                .HasMaxLength(25)
                .HasColumnName("operation")
                .UseCollation("utf8mb3_bin");
            entity.Property(e => e.Params)
                .HasMaxLength(255)
                .HasColumnName("params")
                .UseCollation("utf8mb3_bin");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<MsSysUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("ms_sys_user")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Account)
                .HasMaxLength(64)
                .HasComment("账号")
                .HasColumnName("account");
            entity.Property(e => e.Admin)
                .HasComment("是否管理员")
                .HasColumnType("bit(1)")
                .HasColumnName("admin");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .HasComment("头像")
                .HasColumnName("avatar");
            entity.Property(e => e.CreateDate)
                .HasComment("注册时间")
                .HasColumnName("create_date");
            entity.Property(e => e.Deleted)
                .HasComment("是否删除")
                .HasColumnType("bit(1)")
                .HasColumnName("deleted");
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .HasComment("邮箱")
                .HasColumnName("email");
            entity.Property(e => e.LastLogin)
                .HasComment("最后登录时间")
                .HasColumnName("last_login");
            entity.Property(e => e.MobilePhoneNumber)
                .HasMaxLength(20)
                .HasComment("手机号")
                .HasColumnName("mobile_phone_number");
            entity.Property(e => e.Nickname)
                .HasMaxLength(255)
                .HasComment("昵称")
                .HasColumnName("nickname");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .HasComment("密码")
                .HasColumnName("password");
            entity.Property(e => e.Salt)
                .HasMaxLength(255)
                .HasComment("加密盐")
                .HasColumnName("salt");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasComment("状态")
                .HasColumnName("status");
        });

        modelBuilder.Entity<MsTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("ms_tag")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .HasColumnName("avatar")
                .UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.TagName)
                .HasMaxLength(255)
                .HasColumnName("tag_name")
                .UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
