using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using echo.BLL;
using echo.BLL.Info;
using echo.BLL.Orders;

public partial class Admin_Info_old : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Title = "Прочие настройки";
        Master.HeaderText = "Прочие настройки";
        if(!IsPostBack)
        {
            txtDate.Text = DateTime.Now.ToShortDateString();
        }
    }
    protected void lbtnOrderStatus_Click(object sender, EventArgs e)
    {
        uppnlOrderStatus.Visible = true;
        uppnlNews.Visible = false;
        uppnlRate.Visible = false;
        uppnlNews2.Visible = false;
        BindOrderStatuses();

    }

    protected void BindOrderStatuses()
    {
        lblErrorDelStat.Visible = false;
        using(OrderStatusRepository lRepository=new OrderStatusRepository())
        {
            List<OrderStatus> orderStatuses = lRepository.GetOrderStatuses();
            gvwOrderStatus.DataSource = orderStatuses;
            gvwOrderStatus.DataBind();
        }
    }
    protected void gvwOrderStatus_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvwOrderStatus.EditIndex = e.NewEditIndex;
        BindOrderStatuses();
    }
    protected void gvwOrderStatus_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvwOrderStatus.EditIndex = -1;
        BindOrderStatuses();
    }
    protected void btnStatusAdd_Click(object sender, EventArgs e)
    {
        AddStatus();
    }

    protected void AddStatus()
    {
        using(OrderStatusRepository lRepository=new OrderStatusRepository())
        {
            OrderStatus orderStatus=new OrderStatus();
            orderStatus.OrderStaus = txtStatusAdd.Text;

            orderStatus = lRepository.AddOrderStatus(orderStatus);
        }
        txtStatusAdd.Text = "";
        BindOrderStatuses();
    }
    protected void gvwOrderStatus_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int statusId = (int) gvwOrderStatus.DataKeys[e.RowIndex].Value;
        string newStatusName = (gvwOrderStatus.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;
        using (OrderStatusRepository lRepository=new OrderStatusRepository())
        {
            OrderStatus orderStatus = lRepository.GetOrderStatusById(statusId);
            orderStatus.OrderStaus = newStatusName;

            orderStatus = lRepository.AddOrderStatus(orderStatus);
        }
        gvwOrderStatus.EditIndex = -1;
        BindOrderStatuses();
    }
    protected void gvwOrderStatus_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int statusId = (int)gvwOrderStatus.DataKeys[e.RowIndex].Value;
        using (OrderStatusRepository lRepository = new OrderStatusRepository())
        {
            if (!lRepository.OrdersHaveThisOrderStatus(statusId))
            {
                lRepository.DeleteOrderStatus(statusId);
                BindOrderStatuses();
            }
            else
            {
                lblErrorDelStat.Visible = true;
            }
        }
    }
    protected void gvwOrderStatus_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && gvwOrderStatus.EditIndex==-1)
        {
            int statusId = (int) gvwOrderStatus.DataKeys[e.Row.RowIndex].Value;
            LinkButton btnDel = e.Row.Cells[2].Controls[2] as LinkButton;
            LinkButton btnEdit = e.Row.Cells[2].Controls[0] as LinkButton;
            if(statusId<=3)
            {
                btnDel.Visible = false;
                btnEdit.Visible = false;
            }
            btnDel.OnClientClick = "if (confirm('Вы уверенны что хотите удалить этот раздел?')==false)return false;";

        }
    }

    protected void BindNews()
    {
        lblNewsStatus.Text = "";
        using (InformationRepository repository=new InformationRepository())
        {
            txtNews.Text = repository.GetInfo();
        }
    }

    protected void btnUpdateNews_Click(object sender, EventArgs e)
    {
        UpdateNews();
    }

    protected void UpdateNews()
    {
        using (InformationRepository repository=new InformationRepository())
        {
            if(repository.UpdateInfo(txtNews.Text))
            {
                lblNewsStatus.ForeColor = System.Drawing.Color.Green;
                lblNewsStatus.Visible = true;
                lblNewsStatus.Text = "Текст успешно обновлен";
            }
            else
            {
                lblNewsStatus.ForeColor = System.Drawing.Color.Red;
                lblNewsStatus.Visible = true;
                lblNewsStatus.Text = "Текст не обновлен";
            }
        }
    }
    protected void lbtnNews_Click(object sender, EventArgs e)
    {
        uppnlOrderStatus.Visible = false;
        uppnlNews.Visible = true;
        uppnlRate.Visible = false;
        uppnlNews2.Visible = false;
        BindNews();
    }
    protected void lbtnRate_Click(object sender, EventArgs e)
    {
        uppnlOrderStatus.Visible = false;
        uppnlNews.Visible = false;
        uppnlRate.Visible = true;
        uppnlNews2.Visible = false;
        BindRates();
    }

    protected void lbtnNews2_Click(object sender, EventArgs e)
    {
        uppnlOrderStatus.Visible = false;
        uppnlNews.Visible = false;
        uppnlRate.Visible = false;
        uppnlNews2.Visible = true;
        BindNews2();
    }

    protected void BindRates()
    {
        using (RateRepository lRepository=new RateRepository())
        {
            List<Rate> rates = lRepository.GetRates();
            lvRates.DataSource = rates;
            lvRates.DataBind();
        }
    }
    protected void lvRates_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lvRates.EditIndex = e.NewEditIndex;
        BindRates();
    }
    protected void lvRates_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        int id = int.Parse(lvRates.DataKeys[e.ItemIndex].Value.ToString());
        decimal currency = decimal.Parse(((TextBox) lvRates.Items[e.ItemIndex].FindControl("txtRate")).Text);
        UpdateRate(id,currency);
        lvRates.EditIndex = -1;
        BindRates();
    }
    protected void lvRates_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lvRates.EditIndex = -1;
        BindRates();
    }

    protected void UpdateRate(int id, decimal  currency)
    {
        using(RateRepository lRepository=new RateRepository())
        {
            lRepository.UpdateRate(id, currency);
        }
    }

    protected void BindNews2()
    {
        using (echoNewsRepository erep=new echoNewsRepository())
        {
            List<echoNews> news = erep.GetNews();
            gvwNews.DataSource = news;
            gvwNews.DataBind();
        }
    }

    protected void ClearNews2()
    {
        NewsId = 0;
        gvwNews.SelectedIndex = -1;
        lblNewsEditHeader.Text = "Добавление новости";
        btnEchoNewsAdd.Text = "Добавить";
        txtDate.Text = DateTime.Now.ToShortDateString();
        txtHeader.Text = "";
        txtBody.Text = "";
    }

    protected void UpdateNews2()
    {
        using (echoNewsRepository erep=new echoNewsRepository())
        {
            echoNews news;
            if(NewsId>0)
            {
                news = erep.GetNewsById(NewsId);
            }
            else
            {
                news=new echoNews();
            }
            news.NewsDate = DateTime.Parse(txtDate.Text);
            news.Header = txtHeader.Text;
            news.Body = txtBody.Text;

            news = erep.AddNews(news);
        }
    }

    protected void BindNews2(int newsId)
    {
        using (echoNewsRepository erep=new echoNewsRepository())
        {
            NewsId = newsId;
            echoNews news = erep.GetNewsById(newsId);
            lblNewsEditHeader.Text = string.Format("Редактирование новости за {0}", news.NewsDate.ToShortDateString());
            txtDate.Text = news.NewsDate.ToShortDateString();
            txtHeader.Text = news.Header;
            txtBody.Text = news.Body;
            btnEchoNewsAdd.Text = "Изменить";
        }
        
    }

    protected void DeleteNews2(int groupId)
    {
        using (echoNewsRepository erep= new echoNewsRepository())
        {
            erep.DeleteNews(groupId);
        }
    }

    protected void btnEchoNewsAdd_Click(object sender, EventArgs e)
    {
        UpdateNews2();
        ClearNews2();
        BindNews2();
    }
    protected void btnEchoNewsCancel_Click(object sender, EventArgs e)
    {
        ClearNews2();
    }
    protected void gvwNews_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int newsId = (int) gvwNews.DataKeys[e.NewSelectedIndex].Value;
        BindNews2(newsId);
    }
    protected void gvwNews_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            LinkButton btn = (LinkButton) e.Row.Cells[2].Controls[0];
            btn.OnClientClick = "if(confirm('Вы хотите удалить эту новость?')==false)return false;";
        }
    }
    protected void gvwNews_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int newsId = (int) gvwNews.DataKeys[e.RowIndex].Value;
        DeleteNews2(newsId);
        BindNews2();
    }
}
