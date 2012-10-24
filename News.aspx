<%@ Page Title="" Language="C#" MasterPageFile="~/Theme2.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="News" Theme="Theme2"%>
<%@ MasterType VirtualPath="~/Theme2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
<asp:Repeater ID="rptNews" runat="server">
            <ItemTemplate>
                <div class="news">
                    <span class="date">
                        <%#Eval("NewsDate","{0:d}") %></span>
                    <h4>
                        <%#Eval("Header") %></h4>
                    <p class="sel">
                        <%#Eval("Body") %></p>
                </div>
            </ItemTemplate>
        </asp:Repeater>
</asp:Content>

