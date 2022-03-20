using System;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Core.EventSourcing
{
    public readonly struct Identity
    {
        private readonly Guid _id;

        public Identity(params object[] keys)
        {
            using (MD5 mD = MD5.Create())
            {
                byte[] b = mD.ComputeHash(Encoding.Default.GetBytes(string.Concat(keys)));
                _id = new Guid(b);
            }
        }

        public override string ToString()
        {
            Guid id = _id;
            return id.ToString();
        }

        public static implicit operator Guid(Identity identity)
        {
            return identity._id;
        }

        public static implicit operator Identity(Guid id)
        {
            return new Identity(id);
        }
    }
}
