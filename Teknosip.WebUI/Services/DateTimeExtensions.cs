namespace Teknosip.WebUI.Services
{
	public static class DateTimeExtensions
	{
		public static string ToTimeAgo(DateTime dateTime)
		{
			
			TimeSpan timeSpan = DateTime.UtcNow - dateTime;

			if (timeSpan <= TimeSpan.FromSeconds(60))
				return "Az önce";

			if (timeSpan <= TimeSpan.FromMinutes(60))
				return timeSpan.Minutes > 1 ? $"{timeSpan.Minutes} dakika önce" : "1 dakika önce";

			if (timeSpan <= TimeSpan.FromHours(24))
				return timeSpan.Hours > 1 ? $"{timeSpan.Hours} saat önce" : "1 saat önce";

			if (timeSpan <= TimeSpan.FromDays(7))
				return timeSpan.Days > 1 ? $"{timeSpan.Days} gün önce" : "1 gün önce";

			if (timeSpan <= TimeSpan.FromDays(30))
				return $"{timeSpan.Days / 7} hafta önce";

			return dateTime.ToString("dd MMM yyyy");

		}
	}
}
