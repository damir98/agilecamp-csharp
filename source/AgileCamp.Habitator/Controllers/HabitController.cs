using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgileCamp.Habitator.Models;

namespace AgileCamp.Habitator.Controllers
{
    public class HabitController : Controller
    {
        // GET: Habit
        public ActionResult Index()
        {
            return View("Index", new HabitListVM(new DataStorage().LoadFromDB()));
        }

        public ActionResult Submit(HabitListVM habit)
        {
           new DataStorage().SaveToDB(habit.NewHabit);
            return View("Index", new HabitListVM(new DataStorage().LoadFromDB()));
        }

    }


}