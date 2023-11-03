namespace EfCoreDemo.API.Vo;

public class Result
{
    public string success { get; set; }
    public int code { get; set; }
    public string? msg { get; set; }
    public List<ArticleVo>? data { get; set; }

    public Result() { }
    public Result(List<ArticleVo> data)
    {
        this.success = "success";
        this.code = 200;
        this.data = data;
        this.msg = "请求成功";
    }

    public Result(int code, string msg)
    {
        this.success = "fail";
        this.code = code;
        this.msg = msg;
    }

    public static Result Success(List<ArticleVo> data)
    {
        return new Result(data);
    }

    public static Result Fail(int code, string msg)
    {
        return new Result(code, msg);
    }
}
