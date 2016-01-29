using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileCamp.Habitator.Models
{
    public class Habit
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class HabitListVM 
    {
        public HabitListVM()
        {
        }

        public HabitListVM(List<Habit> habits)
        {
            this.Habits = habits;
        }

        public Habit NewHabit { get; set; }

        public List<Habit> Habits { get; set; }
   }

    public class DataStorage
    {
        private static List<Habit> innerStorage = new List<Habit>()
        {
          new Habit() {Id = 1, Name= "ghbdsxrf" },
                   new Habit() {Id = 1, Name= "ghbdsxrf" }

        };
         
        public List<Habit> LoadFromDB()
        {
            return innerStorage;
        }

        public void SaveToDB(Habit vm)
        {
            innerStorage.Add(vm);
        }
    }
}