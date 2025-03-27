using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Classes
{
    public class PersonalPath
    {
        public int Id { get; private set; }
        public int CurrentPlace {  get; private set; }
        public User Pupil { get; private set; }

        public PersonalPath(User pupil) : this(0, 0, pupil)
        {

        }

        public PersonalPath(int id, int currentPlace, User pupil)
        {
            this.Id = id;
            this.CurrentPlace = currentPlace;
            this.Pupil = pupil;
        }
    }
}
