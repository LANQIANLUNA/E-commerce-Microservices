namespace Mango.Web.Service.IService
{
	public interface ITokenProvider
	{
		void setToken(string token);
		void clearToken();
		string? getToken();
	}
}
