using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class BaseEntity
{
    [Key] public Guid Id { get; set; }
}