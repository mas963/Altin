﻿using Altin.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Altin.DataAccess;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.Property(tl => tl.Title)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(tl => tl.Items)
            .WithOne(ti => ti.List)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
