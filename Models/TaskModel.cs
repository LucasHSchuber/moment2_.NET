using System;
using System.ComponentModel.DataAnnotations;

namespace moment2_mvc.Models
{

  public class TaskModel
  {

    //properties
    public int Id { get; set; }
    [Required]
    [MinLength(2)]
    public string? Name { get; set; }
    [Required]
    public int? Prio { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    [MaxLength(20)]
    [MinLength(5)]
    public string? Title { get; set; }
    [Required]
    [MaxLength(200)]
    [MinLength(5)]
    public string? Description { get; set; }

    public string? Color { get; set; }

  }


}