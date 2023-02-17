using Asp.NetCoreMVCCoding.Datas;
using Asp.NetCoreMVCCoding.Helpers;
using Asp.NetCoreMVCCoding.Models;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;

namespace Asp.NetCoreMVCCoding.Controllers
{
	public class MemberController : Controller
	{
		private readonly DatabaseContext _context;
		private readonly IConfiguration _configuration;
		private readonly IHasher _hasher;

		public MemberController(DatabaseContext context, IConfiguration configuration, IHasher hasher)
		{
			_context = context;
			_configuration = configuration;
			_hasher = hasher;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult MemberListPartial()
		{
			List<User> user = _context.Users.ToList();
			return PartialView("_MemberListPartial", user);
		}

		public IActionResult MemberAddPartial()
		{
			return PartialView("_MemberAddPartial", new CreateViewModel());
		}

		[HttpPost]
		public IActionResult AddNewMember(CreateViewModel model)
		{
			//string hashedPassword = (model.Password + _configuration.GetValue<string>("AppSettings : MD5Salt")).MD5();

			if (ModelState.IsValid)
			{
				if (_context.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
				{
					ModelState.AddModelError("", "Username is already exist.");
					return View(model);
				}

				User user = new User();

				user.Username = model.Username;
				user.FullName = model.FullName;
				user.Password = _hasher.DoMD5HashedPassword(model.Password);
				user.Locked = model.Locked;
				user.Role = model.Role;

				_context.Users.Add(user);
				_context.SaveChanges();

				return PartialView("_MemberAddPartial", new CreateViewModel());

			}

			return PartialView("_MemberAddPartial", model);
		}

		public IActionResult EditMemberPartial(int id)
		{
			User user = _context.Users.FirstOrDefault(x => x.Id == id);

			EditViewModel model = new EditViewModel();
			model.Username = user.Username;
			model.FullName = user.FullName;
			model.Locked = user.Locked;
			model.Role = user.Role;

			return PartialView("_EditMemberPartial", model);
		}

		[HttpPost]
		public IActionResult EditMember(int id, EditViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (_context.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
				{
					ModelState.AddModelError("", "Username already exist.");
					return PartialView("_EditMemberPartial", model);
				}

				User user = _context.Users.FirstOrDefault(x => x.Id == id);

				user.Username = model.Username;
				user.FullName = model.FullName;
				user.Locked = model.Locked;
				user.Role = model.Role;

				_context.SaveChanges();

				return PartialView("_EditMemberPartial", model);
			}

			return PartialView("_EditMemberPartial", model);

		}

		public IActionResult DeleteMemberPartial(int id)
		{
			User user = _context.Users.FirstOrDefault(x => x.Id == id);

			if (user == null)
			{
				ModelState.AddModelError("", "Username have not already exist.");
				
			}

			else
			{
				_context.Users.Remove(user);
				_context.SaveChanges();
			}

			return PartialView("_DeleteMemberPartial", user);
		}
	}
}
