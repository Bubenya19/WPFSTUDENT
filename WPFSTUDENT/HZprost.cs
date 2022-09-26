using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSTUDENT
{
    public class HZprost
    {

        private static HZprost Vibor;
        public static HZprost vibor1
        {
            get
            {
                if (Vibor == null)
                    Vibor = new HZprost();
                return Vibor;
            }
        }
        private static StudentPage stud_page;
        public static StudentPage stud_create
        {
            get 
            { 
                if(stud_page == null)
                    stud_page = new StudentPage();
                return stud_page;
            }
        }

        private static GroupPage group_page;
        public static GroupPage group_create
        {
            get
            {
                if (group_page == null)
                    group_page = new GroupPage();
                return group_page;
            }
        }

        private static SpecPage spec_page;
        public static SpecPage spec_create
        {
            get
            {
                if (spec_page == null)
                    spec_page = new SpecPage();
                return spec_page;
            }
        }








    }
}
