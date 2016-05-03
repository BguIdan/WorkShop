using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ForumBuilder.Common.DataContracts
{
    [DataContract]
    public class User
    {
        [DataMember]
        private String userName { get; set; }

        [DataMember]
        private String password { get; set; }

        [DataMember]
        private String email { get; set; }

        [DataMember]
        private List<String> friends { get; set; }

        public User(String userName, String password, String email)
        {
            this.userName = userName;
            this.password = password;
            this.email = email;
            this.friends = new List<String>();
        }
    }
}
