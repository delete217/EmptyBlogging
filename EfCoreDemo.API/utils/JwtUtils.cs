using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EfCoreDemo.API.utils;

public class JwtUtils
{
    private static string Secret = "!~QSXECGBGSDFFFFDSADDXSS@@_delete_!!";
    private static string Issuer = "delete_is";
    private static string Audience = "delete_au";
    public static string CreateToken(List<Claim> myClaim)
    {
        // 从配置文件中获取密钥
        var secretKey = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret)), SecurityAlgorithms.HmacSha256);

        // 有效载荷
        List<Claim> baseClaims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.Iss,Issuer),
                new Claim(JwtRegisteredClaimNames.Aud,Audience)
        };
        //合并载荷
        myClaim = myClaim.Union<Claim>(baseClaims).ToList<Claim>();

        SecurityToken securityToken = new JwtSecurityToken(
            signingCredentials: secretKey,
            expires: DateTime.Now.AddDays(1),//过期时间
            claims: myClaim
        );
        //生成令牌
        return new JwtSecurityTokenHandler().WriteToken(securityToken);

    }
}
