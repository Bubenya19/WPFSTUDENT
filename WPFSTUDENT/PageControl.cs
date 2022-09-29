using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSTUDENT
{
    public class PageControl
    {
        private static Main_Page mainpage;
        public static Main_Page Main_page
        {
            get
            {
                if (mainpage == null)
                    mainpage = new Main_Page();
                return mainpage;
            }
        }
        private static StudentCreatePage studcr_page;
        public static StudentCreatePage Stud_create
        {
            get
            {
                if (studcr_page == null)
                    studcr_page = new StudentCreatePage();
                return studcr_page;
            }
        }

        private static StudentEditPage studE_page;
        public static StudentEditPage Stud_edit
        {
            get
            {
                if (studE_page == null)
                    studE_page = new StudentEditPage();
                return studE_page;
            }
        }

        private static StudentDeletePage studD_page;
        public static StudentDeletePage Stud_delete
        {
            get
            {
                if (studD_page == null)
                    studD_page = new StudentDeletePage();
                return studD_page;
            }
        }




        private static GroupCreatePage groupcr_page;
        public static GroupCreatePage Group_create
        {
            get
            {
                if (groupcr_page == null)
                    groupcr_page = new GroupCreatePage();
                return groupcr_page;
            }
        }

        private static GroupEditPage groupE_page;
        public static GroupEditPage Group_edit
        {
            get
            {
                if (groupE_page == null)
                    groupE_page = new GroupEditPage();
                return groupE_page;
            }
        }


       




        private static SpecCreatePage speccr_page;
        public static SpecCreatePage Spec_create
        {
            get
            {
                if (speccr_page == null)
                    speccr_page = new SpecCreatePage();
                return speccr_page;
            }
        }





        private static SpecEditPage specE_page;
        public static SpecEditPage Spec_edit
        {
            get
            {
                if (specE_page == null)
                    specE_page = new SpecEditPage();
                return specE_page;
            }
        }

        private static SpecDeletePage specD_page;
        public static SpecDeletePage Spec_delete
        {
            get
            {
                if (specD_page == null)
                    specD_page = new SpecDeletePage();
                return specD_page;
            }
        }
    }
}
