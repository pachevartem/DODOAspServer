using System;

namespace DodoDataModel
{
    public class User
    {
        public Guid guid;
        public string keyRoom;

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(User))
            {
                return false;
            }
            else
            {
              return   guid.Equals(((User)obj).guid);
            }
        }
    }
}
