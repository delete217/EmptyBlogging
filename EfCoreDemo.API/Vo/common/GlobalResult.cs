
namespace EfCoreDemo.API.Vo.common;

public class GlobalResult
{
    public string success { get; set; }
    public int code { get; set; }
    public string? msg { get; set; }
    public object? data { get; set; }

    public GlobalResult() { }
    public GlobalResult(object data)
    {
        this.success = "success";
        this.code = 200;
        this.data = data;
        this.msg = "请求成功";
    }

    public GlobalResult(int code, string msg)
    {
        this.success = "fail";
        this.code = code;
        this.msg = msg;
    }

    public static GlobalResult Success(object data)
    {
        return new GlobalResult(data);
    }

    public static GlobalResult Fail(int code, string msg)
    {
        return new GlobalResult(code, msg);
    }

}
