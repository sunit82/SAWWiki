using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace SAWWiki
{
    public class Test : TestA ,TestB
    {


         

        #region TestB Members

        string TestB.Process()
        {
            return "B";
        }

        #endregion



        #region TestA Members

        string TestA.strA
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        string TestA.Process()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public interface TestA 
    {
        string strA { get; set; }
        string Process();
        
    }
    public interface TestB 
    {
        string Process();
    }
    
}
