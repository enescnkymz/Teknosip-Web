using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Interfaces
{
	public interface IImageService
	{
		Task<string> SaveImageAsWebpAsync(Stream imageStream , string folderName);
		Task DeleteImageAsync(string imageUrl);
	}
}
