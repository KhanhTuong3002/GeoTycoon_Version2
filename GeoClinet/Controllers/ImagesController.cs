using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using static System.IO.File;
using static System.IO.Path;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace GeoClinet.Controllers
{
    [ApiController]
    [Route("/Images")]
    public class ImagesController(PhysicalFileProvider fileProvider) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> UploadImageAsync([FromForm] ImageUploadModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IFormFile file = request.File;
            string ext = GetExtension(file.FileName);
            string fileName = Guid.NewGuid() + ext;
            string filePath = Combine(fileProvider.Root, fileName);

            // Save file to disk
            await using FileStream stream = new(filePath, FileMode.Create);
            await file.CopyToAsync(stream);



            // Generate URL
            string url = Url.Content($"~/Images/{fileName}");

            // Get current user
      /*      var currentUser= await UserManager.GetUserAsync(User) as User;
            Console.WriteLine(currentUser + "-------------------------------------------");
            *//*     if (currentUser == null)
                 {
                     return Unauthorized();
                 }*//*
            // Update user's Avatar field
            currentUser.Avatar = url;
            Console.WriteLine(currentUser + "-------------------------------------------");
            _context.Users.Update(currentUser);
            await _context.SaveChangesAsync();*/

            return Created(url,url);
        }
    }
}
