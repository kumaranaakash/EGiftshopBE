using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EGiftshopBE.Models
{
    public class DAL
    {
        public Response register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", 0);
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Status", 1);
            cmd.Parameters.AddWithValue("@ActionType", "Add");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User registered successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User registration failed";
            }
            return response;
        }
        public Response updateFund(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", users.Email);            
            cmd.Parameters.AddWithValue("@Fund", users.Fund);
            cmd.Parameters.AddWithValue("@ActionType", "AddFund");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Funds updated successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Some error occured. Try after sometime.";
            }
            return response;
        }

        public Response login(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_login", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                response.StatusCode = 200;
                response.StatusMessage = "User is valid";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is invalid";
                response.user = null;
            }
            return response;
        }
        public Response viewUser(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_viewUser", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.Fund = Convert.ToString(dt.Rows[0]["Fund"]);
                user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                response.StatusCode = 200;
                response.StatusMessage = "User exists.";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User does not exist.";
                response.user = null;
            }
            return response;
        }
        public Response updateProfile(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.ID);
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@ActionType", users.actionType);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Record updated successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Some error occured. Try after sometime";
            }

            return response;
        }
        public Response addToCart(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", cart.Email);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@ID", cart.ID);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item addedd successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be added";
            }
            return response;
        }
        public Response cartList(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            List<Cart> listCart = new List<Cart>();
            SqlDataAdapter da = new SqlDataAdapter("sp_CartList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", cart.Email);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cart obj = new Cart();
                    obj.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    obj.ProductName = Convert.ToString(dt.Rows[i]["Name"]); 
                    obj.UnitPrice = Convert.ToInt32(dt.Rows[i]["UnitPrice"]);
                    obj.Discount = Convert.ToInt32(dt.Rows[i]["Discount"]);
                    obj.Quantity = Convert.ToString(dt.Rows[i]["Quantity"]);
                    obj.TotalPrice = Convert.ToInt32(dt.Rows[i]["TotalPrice"]);
                    obj.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
                    listCart.Add(obj);
                }
                if (listCart.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Cart details fetched";
                    response.listCart = listCart;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Cart details are not available";
                    response.listCart = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Cart details are not available";
                response.listCart = null;
            }
            return response;
        }

        public Response placeOrder(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_PlaceOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", users.Email);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order has been placed successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order could not be placed";
            }
            return response;
        }

        public Response orderList(Users users, SqlConnection connection)
        {
            Response response = new Response();
            List<Orders> listOrder = new List<Orders>();
            SqlDataAdapter da = new SqlDataAdapter("sp_OrderList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    order.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                    order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                    order.CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]);
                    order.CustomerName = Convert.ToString(dt.Rows[i]["CustomerName"]);

                    if (users.Type == "UserItems")
                    {
                        order.ProductName = Convert.ToString(dt.Rows[i]["ProductName"]);
                        order.UnitPrice = Convert.ToDecimal(dt.Rows[i]["UnitPrice"]);
                        order.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]);
                        order.TotalPrice = Convert.ToDecimal(dt.Rows[i]["TotalPrice"]);
                        order.CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]);
                        order.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
                    }

                    listOrder.Add(order);
                }
                if (listOrder.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order details fetched";
                    response.listOrders = listOrder;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order details are not available";
                    response.listOrders = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order details are not available";
                response.listOrders = null;
            }
            return response;
        }
        public Response addUpdateProduct(Products products, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddUpdateProduct", connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            List<Products> listProduct = new List<Products>();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", products.ID);
            cmd.Parameters.AddWithValue("@Name", products.Name);
            
            cmd.Parameters.AddWithValue("@UnitPrice", products.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", products.Discount);
            cmd.Parameters.AddWithValue("@Quantity", products.Quantity);
            
            cmd.Parameters.AddWithValue("@ImageUrl", products.ImageUrl);
            cmd.Parameters.AddWithValue("@Status", products.Status);
            cmd.Parameters.AddWithValue("@Type", products.Type);
            if (products.Type != "Get" && products.Type != "GetByID")
            {
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                if (i > 0)
                {
                    response.StatusCode = 200;
                    if (products.Type == "Add")
                        response.StatusMessage = "Product inserted successfully";
                    if (products.Type == "Update")
                        response.StatusMessage = "Product updated successfully";
                    if (products.Type == "Delete")
                        response.StatusMessage = "Product deleted successfully";
                }
                else
                {
                    response.StatusCode = 100;
                    if (products.Type == "Add")
                        response.StatusMessage = "Product did not save. try again.";
                    if (products.Type == "Update")
                        response.StatusMessage = "Product did not update. try again.";
                    if (products.Type == "Delete")
                        response.StatusMessage = "Product did not delete. try again.";
                }
            }
            else
            {
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Products product = new Products();
                        product.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        product.Name = Convert.ToString(dt.Rows[i]["Name"]);
                       
                        product.UnitPrice = Convert.ToString(dt.Rows[i]["UnitPrice"]);
                        product.Discount = Convert.ToString(dt.Rows[i]["Discount"]);
                        product.Quantity = Convert.ToString(dt.Rows[i]["Quantity"]);
                        
                        product.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
                        product.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                        listProduct.Add(product);
                    }
                    if (listProduct.Count > 0)
                    {
                        response.StatusCode = 200;
                        response.listProducts = listProduct;
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.listProducts = null;
                    }
                }
            }

            return response;
        }
        public Response userList(SqlConnection connection)
        {
            Response response = new Response();
            List<Users> listUsers = new List<Users>();
            SqlDataAdapter da = new SqlDataAdapter("sp_UserList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Users user = new Users();
                    user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    user.Password = Convert.ToString(dt.Rows[i]["Password"]);
                    user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    user.Fund = Convert.ToString(dt.Rows[i]["Fund"]);
                    user.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                    user.OrderDate = Convert.ToString(dt.Rows[i]["OrderDate"]);
                    listUsers.Add(user);
                }
                if (listUsers.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "User details fetched";
                    response.listUsers = listUsers;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "User details are not available";
                    response.listUsers = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User details are not available";
                response.listOrders = null;
            }
            return response;
        }
        public Response updateOrderStatus(Orders order, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_updateOrderStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNo", order.OrderNo);
            cmd.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order status has been updated successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Some error occured. Try after sometime.";
            }
            return response;
        }
    }
}
