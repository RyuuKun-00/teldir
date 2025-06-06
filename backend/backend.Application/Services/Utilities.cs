﻿using backend.Core.Models;
using Microsoft.Extensions.Primitives;
using System.Security.Cryptography;
using System.Text;
using UAParser;

namespace backend.Application.Services
{
    public static class Utilities
    {
        public static string Hash(string input)
        {
            var md5 = MD5.Create();
            var sha256 = SHA256.Create();

            var inputByte = Encoding.UTF8.GetBytes(input);

            var result = md5.ComputeHash(sha256.ComputeHash(inputByte));

            return Convert.ToBase64String(result);
        }

        public static UserAgentData GetUserAgentData(StringValues userAgentHeader)
        {
            var uaParser = Parser.GetDefault();
            var clietnInfo = uaParser.Parse(userAgentHeader);

            return new UserAgentData
            {
                OS = clietnInfo.OS.ToString(),
                Browser = clietnInfo.UA.ToString()
            };
        }
    }
}
