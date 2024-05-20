
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;


namespace GeoClinet.Controllers;

public class ImageUploadModel
{
    [Required]
    [DataType(DataType.Upload)]
    [AllowedContentTypes([
        MediaTypeNames.Image.Png, MediaTypeNames.Image.Jpeg, MediaTypeNames.Image.Webp, MediaTypeNames.Image.Svg,
        MediaTypeNames.Image.Gif
    ])]
    public IFormFile File { get; set; } = default!;
/*    [Required]
    public string UserId { get; set; } = default!;*/
}
