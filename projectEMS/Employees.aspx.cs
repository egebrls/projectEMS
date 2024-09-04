using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projectEMS
{
    public partial class Employees : System.Web.UI.Page
    {
        connection dbConn = new connection();
        DataTable getdb = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                DepartmentDropdownAdd();
                string userEmail = Session["LoggedUserEmail"] as string;
                string userRole = Session["LoggedUserRole"] as string;

                if (userRole == "General Manager" || userRole == "Manager")
                {
                    lblEmpSalaryHdr.Visible = true;
                    lblEmpSalary.Visible = true;
                    lblEmpHireDateHdr.Visible = true;
                    lblEmpHireDate.Visible = true;

                    if (userRole == "General Manager")
                    {
                        gvEmployees.PageSize = 5;
                        btnAddEmployee.Visible = true;
                        btnDeleteEmployee.Visible = true;
                        btnEditEmployee.Visible = true;
                    }
                }
                getdb = getEmployee();
                gvEmployees.DataSource = getdb;
                gvEmployees.DataBind();
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string sortOption = ddlistFilter.SelectedItem.Text;
            string filterName = txtFilterName.Text;

            getdb = getEmployee(filterName, sortOption);
            gvEmployees.DataSource = getdb;
            gvEmployees.DataBind();
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            pnlInsertInfo.Visible = true;
            //addContainer.Visible = true;
        }

        protected void imgbtnCloseAdd_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSalary.Text = string.Empty;
            txtHireDate.Text = string.Empty;
            lblErrorMsg.Visible = false;
            lblInsertMsg.Visible = false;
            pnlInsertInfo.Visible = false;
        }

        protected void btnInsertEmployee_Click(Object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;
            string department = ddlDepartment.SelectedItem.Text;
            string position = ddlPosition.SelectedItem.Text;
            string hireDate = txtHireDate.Text;
            string salary = txtSalary.Text;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(department) || string.IsNullOrEmpty(position) || string.IsNullOrEmpty(hireDate))
            {
                lblErrorMsg.Visible = true;
                return;
            }

            addEmployee(firstName, lastName, phone, email, department, position, hireDate, salary);

            lblErrorMsg.Visible = false;

            getdb = getEmployee();
            gvEmployees.DataSource = getdb;
            gvEmployees.DataBind();

            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtHireDate.Text = string.Empty;
            txtSalary.Text = string.Empty;

            lblInsertMsg.Text = "Employee added.";
            lblInsertMsg.Visible = false;
            lblInsertMsg.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "HideUpdateMessage", "setTimeout(hideAddMessage, 3000);", true);
            
        }

        protected void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            btnDeleteEmployee.Visible = false;
            btnEditEmployee.Visible = false;
            lblConfirm.Visible = true;
            btnYes.Visible = true;
            btnNo.Visible = true;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            int selectedEmpID = Convert.ToInt32(Session["selectedEmpID"].ToString());
            deleteEmployee(selectedEmpID);

            getdb = getEmployee();
            gvEmployees.DataSource = getdb;
            gvEmployees.DataBind();

            lblDeleted.Visible = false;
            lblDeleted.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "HideDeleteMessage", "setTimeout(hideDeleteMessage, 3000);", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "HidePanel", "setTimeout(hidePanel, 5000);", true);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            btnDeleteEmployee.Visible = true;
            btnEditEmployee.Visible = true;
            lblConfirm.Visible = false;
            btnYes.Visible = false;
            btnNo.Visible = false;
        }

        protected void imgbtnCloseInfo_Click(Object sender, EventArgs e)
        {
            pnlEmpInfo.Visible = false;
            resetPanel();
        }

        protected void btnEditEmployee_Click(object sender, EventArgs e)
        {
            btnDeleteEmployee.Visible = false;
            btnEditEmployee.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            lblEmpDepartment.Visible = false;
            ddlistEmpDepartmentEdit.Visible = true;
            lblEmpPosition.Visible = false;
            ddlistEmpPositionEdit.Visible = true;
            lblEmpSalary.Visible = false;
            txtEmpSalaryEdit.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            resetPanel();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string depName = ddlistEmpDepartmentEdit.SelectedItem.ToString();
            string position = ddlistEmpPositionEdit.SelectedItem.ToString();
            string salary = txtEmpSalaryEdit.Text;
            editEmployee(depName, position, salary);

            int selectedIndex = gvEmployees.SelectedIndex;
            GridViewRow selectedRow = gvEmployees.Rows[selectedIndex];
            string selectedEmpIDstr = selectedRow.Cells[0].Text;
            int selectedEmpID = Convert.ToInt32(selectedEmpIDstr);
            Session["selectedEmpID"] = selectedEmpID;

            getEmployeeDetails(selectedEmpID);
            resetPanel();
            getdb = getEmployee();
            gvEmployees.DataSource = getdb;
            gvEmployees.DataBind();
        }

        public DataTable getEmployee(string filterName = null, string sortBy = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand("spEmployees"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@action", "getEmployee");
                    if (!string.IsNullOrEmpty(filterName))
                    {
                        cmd.Parameters.AddWithValue("@filterName", filterName);
                    }
                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        cmd.Parameters.AddWithValue("@sortBy", sortBy);
                    }
                    cmd.Connection = conn;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);

                        sda.Dispose();
                    }
                    cmd.Dispose();
                }
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        private void addEmployee(string firstName, string lastName, string phone, string email, string department, string position, string hireDate, string salary = null)
        {
            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand("spEmployees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@action", "addEmployee");
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@depName", department);
                    cmd.Parameters.AddWithValue("@position", position);
                    if (!string.IsNullOrEmpty(salary))
                    {
                        cmd.Parameters.AddWithValue("@salary", Convert.ToDecimal(salary));
                    }
                    cmd.Parameters.AddWithValue("@hireDate", Convert.ToDateTime(hireDate));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        private void DepartmentDropdownAdd()
        {
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("", ""));
            ddlistEmpDepartmentEdit.Items.Clear();
            ddlistEmpDepartmentEdit.Items.Add(new ListItem("", ""));

            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT DepartmentName from tblDepartments ORDER BY DepartmentName", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ddlDepartment.Items.Add(reader["DepartmentName"].ToString());
                            ddlistEmpDepartmentEdit.Items.Add(reader["DepartmentName"].ToString());
                        }
                    }
                }
            }
        }

        protected void gvEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            getdb = getEmployee(txtFilterName.Text, ddlistFilter.SelectedItem.Text);
            gvEmployees.PageIndex = e.NewPageIndex;
            gvEmployees.DataSource = getdb;
            gvEmployees.DataBind();
        }

        protected void gvEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = gvEmployees.SelectedIndex;
            GridViewRow selectedRow = gvEmployees.Rows[selectedIndex];
            string selectedEmpIDstr = selectedRow.Cells[0].Text;
            int selectedEmpID = Convert.ToInt32(selectedEmpIDstr);
            Session["selectedEmpID"] = selectedEmpID;
            string selectedEmpDep = selectedRow.Cells[3].Text;

            getEmployeeDetails(selectedEmpID);
            getEmployeeProjects(selectedEmpID);

            pnlEmpInfo.Visible = true;
            resetPanel();
            if (Session["LoggedUserRole"].ToString() == "Manager" && Session["LoggedEmpDep"].ToString() != selectedEmpDep)
            {
                btnEditEmployee.Visible = false;
            }
        }

        private void getEmployeeDetails(int empIDint)
        {
            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand("spProfile", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@action", "getDetails");
                    cmd.Parameters.AddWithValue("@empID", empIDint);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string imgPath = reader["ProfilePicURL"].ToString();
                            string empID = reader["EmployeeID"].ToString();
                            string firstName = reader["FirstName"].ToString();
                            string lastName = reader["LastName"].ToString();
                            string phone = reader["Phone"].ToString();
                            string email = reader["eMail"].ToString();
                            string depName = reader["DepartmentName"].ToString();
                            string position = reader["Position"].ToString();
                            string salary = reader["Salary"].ToString();
                            DateTime hireDate = (DateTime)reader["HireDate"];

                            imgPic.ImageUrl = imgPath;
                            lblEmpID.Text = empID;
                            lblEmpFirstName.Text = firstName;
                            lblEmpLastName.Text = lastName;
                            lblEmpPhone.Text = phone;
                            lblEmpEmail.Text = email;
                            lblEmpDepartment.Text = depName;
                            lblEmpPosition.Text = position;
                            lblEmpSalary.Text = salary.ToString();
                            lblEmpHireDate.Text = hireDate.ToString("dd-MM-yyyy");
                        }
                        conn.Close();
                    }
                }
            }
        }

        private void deleteEmployee(int empIDint)
        {
            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand("spEmployees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@action", "deleteEmployee");
                    cmd.Parameters.AddWithValue("@empID", empIDint);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        private void editEmployee(string depName = null, string position = null, string salary = null)
        {
            int selectedIndex = gvEmployees.SelectedIndex;
            GridViewRow selectedRow = gvEmployees.Rows[selectedIndex];
            string selectedEmpIDstr = selectedRow.Cells[0].Text;
            int selectedEmpID = Convert.ToInt32(selectedEmpIDstr);
            Session["selectedEmpID"] = selectedEmpID;

            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand("spEmployees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@action", "editEmployee");
                    cmd.Parameters.AddWithValue("@empID", selectedEmpID);
                    if (!string.IsNullOrEmpty(depName))
                    {
                        cmd.Parameters.AddWithValue("@depName", depName);
                    }
                    if (!string.IsNullOrEmpty(position))
                    {
                        cmd.Parameters.AddWithValue("@position", position);
                    }
                    if (!string.IsNullOrEmpty(salary))
                    {
                        cmd.Parameters.AddWithValue("@salary", salary);
                    }

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        private void resetPanel()
        {
            if (Session["LoggedUserRole"].ToString() == "General Manager")
            {
                btnDeleteEmployee.Visible = true;
                btnEditEmployee.Visible = true;
                lblEmpSalary.Visible = true;
            }
            if (Session["LoggedUserRole"].ToString() == "Manager")
            {
                lblEmpSalary.Visible = true;

                if (Session["LoggedEmpDep"].ToString() == lblEmpDepartment.Text)
                {
                    btnEditEmployee.Visible = true;
                }
            }

            lblConfirm.Visible = false;
            btnYes.Visible = false;
            btnNo.Visible = false;
            btnCancel.Visible = false;
            btnSave.Visible = false;
            ddlistEmpDepartmentEdit.Visible = false;
            lblEmpDepartment.Visible = true;
            ddlistEmpPositionEdit.Visible = false;
            lblEmpPosition.Visible = true;
            txtEmpSalaryEdit.Visible = false;
            txtEmpSalaryEdit.Text = string.Empty;
        }

        private void getEmployeeProjects(int empIDint)
        {
            List<string> projectList = new List<string>();

            using (SqlConnection conn = dbConn.dbBag())
            {
                using (SqlCommand cmd = new SqlCommand("spProjects", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@action", "getEmpPrj");
                    cmd.Parameters.AddWithValue("@empID", empIDint);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string projectName = reader["ProjectName"].ToString();
                            projectList.Add(projectName);
                        }
                    }
                }
            }
            string projectNames = string.Join(", ", projectList);

            lblEmpPrj.Text = "<strong>Projects Involved: </strong>" + projectNames;

        }

    }
}