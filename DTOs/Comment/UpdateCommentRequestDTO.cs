using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs.Comment
{
    public class UpdateCommentRequestDTO
    {
        [Required]
        [Length(10, 300, ErrorMessage = "Title length must be between 10 and 300 characters long.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [Length(10, 300, ErrorMessage = "Content length must be between 10 and 300 characters long.")]
        public string Content { get; set; } = string.Empty;
    }
}
