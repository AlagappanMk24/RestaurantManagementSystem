using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Domain.Entities;

[Owned]
public class Address
{
    [Required]
    public bool IsPresent { get; set; } = true;

    [MaxLength(50)]
    public string? City { get; set; }

    [MaxLength(100)]
    public string? Street { get; set; }

    [MaxLength(20)]
    public string? PostalCode { get; set; }
}
