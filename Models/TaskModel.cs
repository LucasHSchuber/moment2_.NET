using System;
using System.ComponentModel.DataAnnotations;

namespace moment2_mvc.Models
{

  public class TaskModel
  {

    //properties
    public int Id { get; set; }

    [Required]
    [MaxLength(5)]
    public string? Title { get; set; }
  [Required]
    [MaxLength(10)]
    public string? Description { get; set; }

  }


}