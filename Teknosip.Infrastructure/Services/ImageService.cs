using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Interfaces;



namespace Teknosip.Infrastructure.Services
{
	public class ImageService : IImageService
	{
		private readonly IWebHostEnvironment _env;

		public ImageService(IWebHostEnvironment env)
		{
			_env = env;
		}

		public async Task<string> SaveImageAsWebpAsync(Stream imageStream, string folderName)
		{
			if (imageStream == null || imageStream.Length == 0) 
				return null;

			// 1. Rastgele, benzersiz bir isim oluştur (Örn: 5f4dcc3b5..._profile.webp)
			string uniqueFileName = Guid.NewGuid().ToString() + ".webp";

			// 2. Kaydedilecek klasörün yolunu bul (wwwroot/images/profiles)
			string uploadsFolder = Path.Combine(_env.WebRootPath, "images", folderName);

			// 3. Eğer klasör yoksa, oluştur
			if (!Directory.Exists(uploadsFolder))
			{
				Directory.CreateDirectory(uploadsFolder);
			}

			// 4. Dosyanın tam kaydedileceği yol
			string filePath = Path.Combine(uploadsFolder, uniqueFileName);

			// 5. ImageSharp Sihri: Resmi Stream'den oku, kırp ve WebP olarak kaydet
			using (var image = await Image.LoadAsync(imageStream))
			{
				// Resmi merkezden 500x500 olacak şekilde kırp (Kare profil fotoğrafı)
				image.Mutate(x => x.Resize(new ResizeOptions
				{
					Size = new Size(500, 500),
					Mode = ResizeMode.Crop
				}));

				// WebP formatı ve %80 kalite (Boyutu inanılmaz küçültür, kaliteyi bozmaz)
				var encoder = new WebpEncoder { Quality = 80 };

				await image.SaveAsWebpAsync(filePath, encoder);
			}

			// 6. Veritabanına kaydedilecek olan URL'i geri dön
			return $"/images/{folderName}/{uniqueFileName}";
		}

		public Task DeleteImageAsync(string imageUrl)
		{

			if (string.IsNullOrEmpty(imageUrl) || imageUrl.Contains("default", StringComparison.OrdinalIgnoreCase))
			{
				return Task.CompletedTask;
			}

			// 2. Fiziksel Yolu Bul: Gelen URL "/images/profiles/resim.webp" şeklindedir.
			// Başındaki "/" işaretini siliyoruz ki Path.Combine hata vermesin.
			var relativePath = imageUrl.TrimStart('/');

			// 3. wwwroot yolu ile birleştiriyoruz (Örn: C:\Proje\wwwroot\images\profiles\resim.webp)
			string fullPath = Path.Combine(_env.WebRootPath, relativePath);

			// 4. Dosya gerçekten diskte var mı diye bak, varsa yok et!
			if (File.Exists(fullPath))
			{
				File.Delete(fullPath);
			}

			return Task.CompletedTask; 
		}


	}
}
