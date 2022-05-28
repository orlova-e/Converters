using System;
using Converters.Domain.Interfaces;

namespace Converters.Domain.Entities
{
    public class File : IDomainEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] Data { get; set; }
    }
}