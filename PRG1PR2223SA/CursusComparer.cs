using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class CursusVolgensTitelOplopendComparer : IComparer<Cursus>
    {
        public int Compare(Cursus cursus1, Cursus cursus2)
        {
            return string.Compare(cursus1.Titel, cursus2.Titel);
        }
    }
    public class CursusVolgensStudiepuntenOplopendComparer : IComparer<Cursus>
    {
        public int Compare(Cursus cursus1, Cursus cursus2)
        {
            if (cursus1.Studiepunten < cursus2.Studiepunten)
            {
                return -1;
            }
            else if (cursus1.Studiepunten > cursus2.Studiepunten)
            {
                return +1;
            }
            else
            {
                return 0;
            }
        }
    }
}
