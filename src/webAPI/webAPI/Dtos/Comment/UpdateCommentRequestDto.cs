using System.ComponentModel.DataAnnotations;

namespace webAPI.Dtos.Comment;

public class UpdateCommentRequestDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long")]
    [MaxLength(200, ErrorMessage = "Title must be at most 200 characters long")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(5, ErrorMessage = "Content must be at least 5 characters long")]
    [MaxLength(200, ErrorMessage = "Content must be at most 200 characters long")]
    public string Content { get; set; } = string.Empty;
}