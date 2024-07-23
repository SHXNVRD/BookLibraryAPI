using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class KeyGenerator
    {
        public static RsaSecurityKey GenerateFromXmlFile(string path)
        {
            string xmlString = File.ReadAllText(path);
            var rsa = RSA.Create();
            rsa.FromXmlString(xmlString);
            return new RsaSecurityKey(rsa);
        }
    }
}
