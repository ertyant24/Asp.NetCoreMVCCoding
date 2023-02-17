using NETCore.Encrypt.Extensions;

namespace Asp.NetCoreMVCCoding.Helpers
{
	public interface IHasher
	{
		string DoMD5HashedPassword(string p);
	}

	public class Hasher : IHasher
	{
		private readonly IConfiguration _configuration;

		public Hasher(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string DoMD5HashedPassword(string p)
		{
			string MD5Salt = _configuration.GetValue<string>("AppSettings : MD5Salt");
			string salted = p + MD5Salt;
			string hashed = salted.MD5();

			return hashed;
		}
	}

}
