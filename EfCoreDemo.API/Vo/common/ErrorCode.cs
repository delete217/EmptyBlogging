namespace EfCoreDemo.API.Vo.common;

public enum ErrorCode : int
{
    PARAMS_ERROR = 10001,
    ACCOUNT_NOT_EXIST = 10002,
    NOT_LOGIN = 1003,
    UNAUTHORIZED = 1004,
    TOKEN_NOT_LEGAL = 1005,

    Request_NOT_ALLOWED = 1006,
    PWD_NOT_CORRECT = 1007,

}

public static class ErrorCodeExtensions
{
    public static string GetMessage(this ErrorCode errorCode)
    {
        switch (errorCode)
        {
            case ErrorCode.PARAMS_ERROR:
                return "用户名或密码错误";
            case ErrorCode.ACCOUNT_NOT_EXIST:
                return "用户名不存在";
            case ErrorCode.NOT_LOGIN:
                return "未登录！";
            case ErrorCode.TOKEN_NOT_LEGAL:
                return "token不合法";
            case ErrorCode.UNAUTHORIZED:
                return "权限不足";
            case ErrorCode.Request_NOT_ALLOWED:
                return "请求被拦截";
            case ErrorCode.PWD_NOT_CORRECT:
                return "密码错误";
            default:
                return "未知错误";
        }
    }
}



