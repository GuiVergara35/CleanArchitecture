using System;

namespace CleanArchMvc.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
