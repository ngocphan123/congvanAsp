namespace CongVan.Areas.Admin.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NLP : DbContext
    {
        public NLP()
            : base("name=NLP")
        {
        }

        public virtual DbSet<assignment> assignments { get; set; }
        public virtual DbSet<cat> cats { get; set; }
        public virtual DbSet<department> departments { get; set; }
        public virtual DbSet<department_cat> department_cat { get; set; }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<follow> follows { get; set; }
        public virtual DbSet<groups_user> groups_user { get; set; }
        public virtual DbSet<row> rows { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<type> types { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<assignment>()
                .Property(e => e.result)
                .IsUnicode(false);

            modelBuilder.Entity<cat>()
                .Property(e => e.introduction)
                .IsUnicode(false);

            modelBuilder.Entity<cat>()
                .HasMany(e => e.rows)
                .WithOptional(e => e.cat)
                .HasForeignKey(e => e.idcat);

            modelBuilder.Entity<department>()
                .HasMany(e => e.assignments)
                .WithOptional(e => e.department)
                .HasForeignKey(e => e.id_department);

            modelBuilder.Entity<department>()
                .HasMany(e => e.employees)
                .WithRequired(e => e.department)
                .HasForeignKey(e => e.iddepart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<department>()
                .HasMany(e => e.follows)
                .WithOptional(e => e.department)
                .HasForeignKey(e => e.id_department);

            modelBuilder.Entity<department_cat>()
                .HasMany(e => e.departments)
                .WithOptional(e => e.department_cat)
                .HasForeignKey(e => e.depcatid);

            modelBuilder.Entity<follow>()
                .Property(e => e.list_timeview)
                .IsUnicode(false);

            modelBuilder.Entity<groups_user>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<groups_user>()
                .HasMany(e => e.users)
                .WithOptional(e => e.groups_user)
                .HasForeignKey(e => e.group_id);

            modelBuilder.Entity<row>()
                .Property(e => e._abstract)
                .IsUnicode(false);

            modelBuilder.Entity<row>()
                .Property(e => e.note)
                .IsUnicode(false);

            modelBuilder.Entity<row>()
                .HasMany(e => e.assignments)
                .WithOptional(e => e.row)
                .HasForeignKey(e => e.id_dispatch);

            modelBuilder.Entity<row>()
                .HasMany(e => e.follows)
                .WithOptional(e => e.row)
                .HasForeignKey(e => e.id_dispatch);

            modelBuilder.Entity<type>()
                .HasMany(e => e.rows)
                .WithOptional(e => e.type1)
                .HasForeignKey(e => e.type);

            modelBuilder.Entity<user>()
                .HasMany(e => e.employees)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);
        }
    }
}
