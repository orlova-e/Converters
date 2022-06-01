using System;
using System.Reflection;
using Converters.Domain.Interfaces;

namespace Converters.Domain.Entities
{
    public class Convertation : IDomainEntity, IHistorical
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public string Name { get; set; }
        public Guid SessionId { get; set; }
    }
}