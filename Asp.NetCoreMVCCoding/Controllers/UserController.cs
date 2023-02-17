using Asp.NetCoreMVCCoding.Datas;
using Asp.NetCoreMVCCoding.Helpers;
using Asp.NetCoreMVCCoding.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCoreMVCCoding.Controllers
{
	public class UserController : Controller
	{
		private readonly DatabaseContext _context;
		private readonly IHasher _hasher;

		public UserController(DatabaseContext context, IHasher hasher)
		{
			_context = context;
			_hasher = hasher;
		}

		public IActionResult Index()
		{
			List<User> user = _context.Users.ToList();

			return View(user);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				if(_context.Users.Any(x => x.Username == model.Username && x.Password == model.Password))
				{
					ModelState.AddModelError("", "Username is already exist.");

					return View(model);
				}

				User user = new User(); ;

				user.FullName = model.FullName;
				user.Username = model.Username;
				user.Role = model.Role;
				user.Locked = model.Locked;
				user.Password = _hasher.DoMD5HashedPassword(model.Password);

				_context.Users.Add(user);
				_context.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			return View(model);
		}

		public IActionResult Edit(int id)
		{
			User user = _context.Users.FirstOrDefault(x => x.Id == id);
			
			if (user != null)
			{
				EditViewModel model = new EditViewModel();

				model.Locked = user.Locked;
				model.Username = user.Username;
				model.Role = user.Role;
				model.FullName = user.FullName;

				return View(model);
			}

			return View();
		}


		[HttpPost]
		public IActionResult Edit(int id, EditViewModel model)
		{
			if (ModelState.IsValid)
			{
				if(_context.Users.Any(x => x.Username == model.Username && x.Id != id))
				{
					ModelState.AddModelError(nameof(model.Username), "Username is already exist.");

					return View(model);
				}

				User user = _context.Users.Find(id);

				if (user != null)
				{
					user.FullName = model.FullName;
					user.Username = model.Username;
					user.Role = model.Role;
					user.Locked = model.Locked;

					_context.SaveChanges();

					return RedirectToAction(nameof(Index));
				}

			}

			return View(model);

			
		}

	
		public IActionResult Delete(int id)
		{
			User user = _context.Users.FirstOrDefault(x => x.Id == id);

			if (user != null)
			{
				_context.Users.Remove(user);
				_context.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			return View();
		}
	}
}
