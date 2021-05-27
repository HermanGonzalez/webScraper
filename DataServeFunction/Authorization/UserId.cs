using System;

namespace DataServeFunction.Authorization
{
    public class UserId
    {
        private readonly Guid? _id;
        public static readonly UserId Unavailable = new UserId(null);

        private UserId(Guid? id)
        {
            _id = id;
        }

        public static UserId Of(Guid? id)
        {
            return new UserId(id);
        }

        public override string ToString()
        {
            return _id.HasValue ? _id.Value.ToString() : "N/A";
        }
    }
}
