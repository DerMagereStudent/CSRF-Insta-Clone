﻿using System.Collections.Immutable;

using CSRFInstaClone.Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace CSRFInstaClone.Infrastructure.Database; 

public class ApplicationDbContext : DbContext {
	public DbSet<UserProfile> UserProfiles { get; set; }
	public DbSet<Post> Posts { get; set; }
	public DbSet<Image> Images { get; set; }
	public DbSet<Like> Likes { get; set; }
	public DbSet<Follower> Followers { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
	
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<UserProfile>(entity => {
			entity.HasKey(up => up.Id);
		});

		modelBuilder.Entity<Post>(entity => {
			entity.HasKey(p => p.Id);

			entity.HasOne<Image>()
				.WithOne()
				.HasForeignKey<Post>(p => p.ImageId);
		});
		
		modelBuilder.Entity<Image>(entity => {
			entity.HasKey(i => i.Id);
		});
		
		modelBuilder.Entity<Like>(entity => {
			entity.HasKey(l => new {l.PostId, l.UserId});
			
			entity.HasOne<Post>()
				.WithOne()
				.HasForeignKey<Like>(l => l.PostId);
		});
		
		modelBuilder.Entity<Follower>(entity => {
			entity.HasKey(f => new {f.UserId, f.FollowerId});
		});
	}
}