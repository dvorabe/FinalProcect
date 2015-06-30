<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Linq;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        var prk = context.Request.QueryString["prk"].ToString();
        var psk = context.Request.QueryString["psk"].ToString();
        var sfr = int.Parse(context.Request.QueryString["sfr"].ToString());

        using (var ctx = new DatabaseEntities())
        {
            var item = ctx.Tanach.Where(t => t.pasuk == psk && t.perek == prk && t.sefer == sfr).FirstOrDefault();
            context.Response.Write(item.txt);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}