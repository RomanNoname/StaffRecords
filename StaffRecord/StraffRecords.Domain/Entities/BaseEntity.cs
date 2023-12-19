﻿namespace StraffRecords.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    }
}
