<%@ Control Language="C#" ClassName="LinksBlock" %>
<%@ Import Namespace="System.Data" %>

<div class="widget">
    <div class="title">
        <span>
            Meta</span></div>
    <div class="content">
        <ul>  <%if (Session["ActiveUser"]==null)
                  {
                %>
            <li><a href="<%=Blogsa.Url %>Register.aspx">Kayıt Ol</a></li>
             <li><a href="<%=Blogsa.Url %>Login.aspx">Giriş</a></li>
             <%}else{%>
              <li><a href="<%=Blogsa.Url %>Login.aspx?logout">Çıkış</a></li>
              <%} %>
              <li><a href="<%=Blogsa.Url %>Feed.aspx">Yazılar RSS</a></li>
               <li><a href="<%=Blogsa.Url %>Feed.aspx?Comments">Yorumlar RSS</a></li>
                <li><a href="http://www.blogsa.net">Blogsa.NET</a></li>
        </ul>
    </div>
</div>
