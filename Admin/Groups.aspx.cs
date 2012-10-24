using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Products;

public partial class Admin_Groups : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Master.Title = "Управление разделами";
            Master.HeaderText = "Управление разделами";
            BindGroups();
        }
    }

    protected void BindGroups()
    {
        using (GroupRepository lRepository = new GroupRepository())
        {
            List<Group> groups = lRepository.GetGroups();
            gvwGroups.DataSource = groups;
            gvwGroups.DataBind();
        }
    }

    protected void BindGroup(int groupId)
    {
        using (GroupRepository lRepository = new GroupRepository())
        {
            GroupId = groupId;
            Group _group = lRepository.GetGroupById(groupId);
            lblEditHeader.Text = string.Format("Редактирование раздела: {0}, ID: {1}", _group.GroupNameRus,
                                               _group.GroupID);
            txtGroupNameRus.Text = _group.GroupNameRus;
            txtGroupNameEng.Text = _group.GroupNameEng;
            txtGroupOrder.Text = _group.GroupOrder.ToString();
            cbxAvaliable.Checked = _group.AvaliableInEngilsh;
            btnAddGroup.Text = "Изменить";
        }
    }

    protected void ClearGroup()
    {
        GroupId = 0;
        gvwGroups.SelectedIndex = -1;
        lblEditHeader.Text = "Добавление нового раздела";
        btnAddGroup.Text = "Добавить";
        txtGroupNameRus.Text = "";
        txtGroupNameEng.Text = "";
        txtGroupOrder.Text = "";
        cbxAvaliable.Checked = true;
    }

    protected void UpdateGroup()
    {
        using (GroupRepository lRepository = new GroupRepository())
        {
            Group _group;
            if (GroupId > 0)
            {
                _group = lRepository.GetGroupById(GroupId);
            }
            else
            {
                _group = new Group();
            }
            _group.GroupNameRus = txtGroupNameRus.Text;
            _group.GroupNameEng = txtGroupNameEng.Text;
            _group.GroupOrder = int.Parse(txtGroupOrder.Text);
            _group.AvaliableInEngilsh = cbxAvaliable.Checked;

            _group = lRepository.AddGroup(_group);
        }
    }

    protected void gvwGroups_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int groupId = (int)gvwGroups.DataKeys[e.NewSelectedIndex].Value;
        BindGroup(groupId);
    }

    protected void gvwGroups_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btn = e.Row.Cells[5].Controls[0] as LinkButton;
            btn.OnClientClick = "if (confirm('Вы уверенны что хотите удалить этот раздел?')==false)return false;";
            btn.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnCancelGroup_Click(object sender, EventArgs e)
    {
        ClearGroup();
    }
    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        UpdateGroup();
        ClearGroup();
        BindGroups();
    }

    protected void DeleteGroup(int groupId)
    {
        using (GroupRepository lRepository = new GroupRepository())
        {
            lRepository.DeleteGroup(groupId);
        }
    }

    protected void gvwGroups_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int groupId = (int)gvwGroups.DataKeys[e.RowIndex].Value;
        DeleteGroup(groupId);
        BindGroups();
    }

}