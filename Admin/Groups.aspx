<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin2.master" AutoEventWireup="true"
    CodeFile="Groups.aspx.cs" Inherits="Admin_Groups" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.5.40412.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ MasterType VirtualPath="~/Admin/Admin2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Список разделов</h3>
    <div class="row">
    <div class="span12">
    <asp:GridView ID="gvwGroups" runat="server" AutoGenerateColumns="False" DataKeyNames="GroupID"
        OnSelectedIndexChanging="gvwGroups_SelectedIndexChanging" OnRowCreated="gvwGroups_RowCreated"
        OnRowDeleting="gvwGroups_RowDeleting" CssClass="table table-striped table-bordered table-condensed" GridLines="None">
        <Columns>
            <asp:BoundField DataField="GroupOrder" HeaderText="Порядок сортировки"  HeaderStyle-CssClass="center" />
            <asp:BoundField DataField="GroupID" HeaderText="GroupID" HeaderStyle-CssClass="center" />
            <asp:BoundField DataField="GroupNameRus" HeaderText="Русское имя" HeaderStyle-CssClass="center"  />
            <asp:BoundField DataField="GroupNameEng" HeaderText="Английское имя" HeaderStyle-CssClass="center" />
            <asp:CheckBoxField DataField="AvaliableInEngilsh" HeaderText="Доступно в англ. версии" HeaderStyle-CssClass="center" />
            <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" DeleteText="Удалить"
                SelectText="Выбрать" />
        </Columns>
    </asp:GridView>
    </div>
    </div>
    <p>
    </p>
    <p>
    </p>
    <asp:Panel ID="pnlEditGroup" runat="server">
        <h3>
            <asp:Label ID="lblEditHeader" runat="server" Text="Добавление нового раздела" /></h3>
            <div class="row">
            <div class="span12">
            <fieldset>
                <div class="control-group">
                    <label class="control-label" for="txtGroupNameRus">Русское имя:</label>
                    <div class="controls">
                        <asp:TextBox ID="txtGroupNameRus" runat="server" CssClass="span5" ClientIDMode="Static" />
                        <asp:RequiredFieldValidator ID="valRequireNameRus" ControlToValidate="txtGroupNameRus"
                        runat="server" ErrorMessage="Требуется вести русское имя" Display="Dynamic" ValidationGroup="EditGroup">*</asp:RequiredFieldValidator>
                     </div>
                </div>
                <div class="control-group">
                   <label class="control-label" for="txtGroupNameEng">Английское имя:</label>
                   <div class="controls">
                       <asp:TextBox ID="txtGroupNameEng" runat="server" CssClass="span5" ClientIDMode="Static" />
                    <asp:RequiredFieldValidator ID="valRequireNameEng" ControlToValidate="txtGroupNameEng"
                        runat="server" ErrorMessage="Требуется вести английское имя" Display="Dynamic"
                        ValidationGroup="EditGroup">*</asp:RequiredFieldValidator>
                       </div> 
                </div>
                <div class="control-group">
                    <label class="control-label" for="txtGroupOrder">Порядок сортировки:</label>
                    <div class="controls">
                         <asp:TextBox ID="txtGroupOrder" runat="server" CssClass="span1" ClientIDMode="Static"/>
                    <asp:RangeValidator ID="valOrdrRange" runat="server" ErrorMessage="Только целое число" MaximumValue="1000" 
                    MinimumValue="-1000" Type="Integer" ControlToValidate="txtGroupOrder" Display="Dynamic"  ValidationGroup="EditGroup">*
                        </asp:RangeValidator>    
                    </div>
                </div>
                <div class="control-group">
                    <label for="cbxAvaliable" class="control-label">Доступна в английской версии:</label>
                    <div class="controls">
                      <label class="checkbox">
                          <asp:CheckBox ID="cbxAvaliable" runat="server" Checked="true" ClientIDMode="Static"/>
                      </label>
                    &nbsp;</div>
                  </div>
                <div class="form-actions">
                     <asp:Button ID="btnAddGroup" runat="server" Text="Добавить" ValidationGroup="EditGroup"
                        CssClass="btn btn-primary" OnClick="btnAddGroup_Click" />
                    <asp:Button ID="btnCancelGroup" runat="server" Text="Отмена" CssClass="btn"
                        OnClick="btnCancelGroup_Click" />
                </div>
                 <asp:ValidationSummary ID="valSum" ValidationGroup="EditGroup" runat="server" ShowMessageBox="false"
                        ShowSummary="true" CssClass="alert alert-error span3"/>
            </fieldset>
            </div>
            </div>
    </asp:Panel>
</asp:Content>
